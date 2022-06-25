using MSharp;

namespace Modules
{
    class CommodityList : BaseListModule<Domain.Commodity>
    {
        public CommodityList()
        {
            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");
            DataSource("await info.Consignment.Commodities.OrderBy(t => t.SubmitDate).GetList()");
            ShowFooterRow(true);
            ButtonsLocation("Bottom");

            Column(x => x.Product).LabelText("Product");

            Column(x => x.GrossWeight).DisplayFormat("{0:#,0.##}")
                .FooterMarkup("Total: @(\"{0:#,0.##}\".FormatWith(await info.Consignment.GetCurrentGrossWeight()))");
            Column(x => x.NetWeight).DisplayFormat("{0:#,0.##}")
                .FooterMarkup("Total: @(\"{0:#,0.##}\".FormatWith(await info.Consignment.GetCurrentNetWeight()))");
            CustomColumn("Currency").LabelText("Currency").DisplayExpression("@info.Consignment.InvoiceCurrency");
            Column(x => x.Value).DisplayFormat("{0:#,0.##}")
                .FooterMarkup("Total: @(\"{0:#,0.##}\".FormatWith(await info.Consignment.GetCurrentValue()))");
            Column(x => x.NumberOfPackages).LabelText("Number of packages").DisplayFormat("{0:#,0}")
                .FooterMarkup("Total: @(\"{0:#,0}\".FormatWith(await info.Consignment.GetCurrentNumberOfPackages()))");

            Column(x => x.CountryOfDestination)
                .IsSortable(false)
                .LabelText("@(info.Consignment.Shipment.IsInUK ? \"Country of origin\" : \"Country of destination\")");

            Column(x => x.PreferenceType).LabelText("Preference");

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit).VisibleIf("@info.Consignment.IsEditable(GetUser())")
                .OnClick(x =>
                {
                    x.Go<Share.Commodities.CommodiyEnterPage>().Send("item", "item.ID").Pass("consignment").SendReturnUrl();
                });

            ButtonColumn("Delete").HeaderText("Delete").Text("Delete")
               .GridColumnCssClass("actions")
               .ConfirmQuestion("Are you sure you want to delete this commodity?")
               .CssClass("btn-danger")
               .Icon(FA.Remove)
               .VisibleIf("@info.Consignment.IsEditable(GetUser())")
               .OnClick(x =>
               {
                   x.DeleteItem();
                   x.CSharp(@"try { await info.Consignment.ResetStatus(); } catch(Exception ex) { return Notify(ex.Message, ""error""); }");
                   x.RefreshPage();
               });

            Button("Transmit").VisibleIf("info.Consignment.IsEditable(GetUser()) && info.Items.Any() && info.Consignment.Shipment.IsOutUK")
            .ExtraTagAttributes("@(info.ShouldShowWarning?  Html.Raw(\"data-confirm-question='Please check the weight as it seems too high for a road vehicle.' data-confirm-ok='Confirm'\") : null))")
            .OnClick(x =>
            {
                x.CSharp(@"try { await info.Consignment.FlagAsCompleted(); }
                        catch(Exception ex) { return Notify(ex.Message, ""error""); }");
                x.Go<Share.Consignments.ConsignmentsPage>().Send("shipment", "info.Consignment.Shipment.ID");
            });

            Button("Complete")
                .VisibleIf("info.Consignment.IsEditable(GetUser()) && info.Items.Any() && info.Consignment.Shipment.IsInUK")
              .ExtraTagAttributes("@(info.ShouldShowWarning?  Html.Raw(\"data-confirm-question='Please check the weight as it seems too high for a road vehicle.' data-confirm-ok='Confirm'\") : null))")
              .OnClick(x =>
              {
                  x.If("info.Consignment.UCN.IsEmpty() && info.Consignment.Shipment.IsInUK && info.Consignment.IntoUKType == PortType.Inventory").CSharp(@"return Notify(""UCN value is required"", ""error"");");
                  x.If("info.Consignment.IsHaveAllDeclaraionDetails() && await info.Consignment.Commodities.GetList().Any(x => x.HasDutyPayable)")
                   .CSharp(@"if((await info.Consignment.DutyMessage()).StartsWith(""Please arrange to top up"")){
                             Redirect(Url.Index(""CommoditiesDerermentNumberWarning"", new { consignment = info.Consignment.ID }), target: ""$modal"");}
                           else {  
                             Redirect(Url.Index(""CommoditiesDutyIsPayableView"", new { consignment = info.Consignment.ID }), target: ""$modal"");}
                   ");
                  x.Else()
                  .CSharp(@"try { await info.Consignment.FlagAsCompleted(); }
                        catch(Exception ex) { return Notify(ex.Message, ""error""); }");
                  x.Else().Go<Share.Consignments.ConsignmentsPage>().Send("shipment", "info.Consignment.Shipment.ID");
              });

            ViewModelProperty<bool>("ShouldShowWarning")
                .OnBound(@"var confirmedConsignments = (await info.Consignment.Shipment.Consignments.GetList()).Where(x => x.AdminStatusLabel.IsAnyOf(Helper.Transmitables.Select(y => y.AdminDisplay)));
            if (confirmedConsignments.Sum(x => x.TotalGrossWeight) + info.Consignment.TotalGrossWeight > 30000)
                info.ShouldShowWarning = true;");

    }
}
}