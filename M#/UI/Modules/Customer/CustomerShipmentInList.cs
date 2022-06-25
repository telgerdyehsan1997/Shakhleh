using MSharp;

namespace Modules
{
    class CustomerShipmentInList : BaseListModule<Domain.Shipment>
    {
        public CustomerShipmentInList()
        {
            RootCssClass("mfa");
            this.AddDependency<Domain.IMFAService>();
            this.AddDependency<Domain.ISmsService>();

            HeaderText("Shipments Into UK")
                .DataSource(@"await FilterShipments(info, CompanyUser.Company)");

            Search(x => x.TrackingNumber)
                .MemoryFilterCode("//remove from here");

            Search(x => x.IsNCTSShipmentOutConvertible)
                .Label("Has NCTS")
                .Control(ControlType.HorizontalRadioButtons)
                .MemoryFilterCode("//remove from here");

            Search(x => x.Date)
                .Label("Date created")
                .VisibleIf("!info.TrackingNumber.HasValue() && !info.MyReferenceForCPInvoice.HasValue()")
                .MemoryFilterCode("//remove from here");


            Search(x => x.ExpectedDate)
                .Label("Expected date of arrival/departure")
                .VisibleIf("!info.TrackingNumber.HasValue() && !info.MyReferenceForCPInvoice.HasValue()")
                .MemoryFilterCode("//remove from here");


            Search(x => x.MyReferenceForCPInvoice).Label("Customer Reference")
                .MemoryFilterCode("//remove from here");

            Search(x => x.VehicleNumber).MemoryFilterCode("//remove from here");
            Search(x => x.TrailerNumber).MemoryFilterCode("//remove from here");


            this.AddAdminProgressFilter().MemoryFilterCode("//remove from here");

            this.ArchiveSearch().DefaultValueExpression("false").MemoryFilterCode("//remove from here");

            var shipmentButton = SearchButton("Clear Shipment Level Search").OnClick(x =>
            {
                x.CSharp("await SearchUrlCookie.ClearCookieHistory(isNcts: false, isIntoUk: true);");
                x.Go<Customer.ShipmentsIntoUKPage>();
            });

            var consigmentButton = SearchButton("Clear Consignment Level Search").OnClick(x =>
            {
                x.CSharp("await ConsignmentSearch.ClearCookieHistory(isNcts: false, isIntoUk: true );");
                x.Go<Customer.ShipmentsIntoUKPage>();
            });

            Box("ShipmentHeader", BoxTemplate.HeaderBox)
                .Text($"Shipment Level Search Filter: Active {shipmentButton.Ref}")
                .Visibility("await SearchUrlCookie.IsActiveShipmentSearch(isNcts: false, isIntoUk: true)");

            Box("ConsignmentHeader", BoxTemplate.HeaderBox)
               .Text($"Consignment Level Search Filter: Active {consigmentButton.Ref}")
               .Visibility("await SearchUrlCookie.IsActiveConsignmentSearch(isNcts: false, isIntoUk: true)");


            SearchButton("Search").Icon(FA.Search).OnClick(x => { x.ReturnView(); }).IsDefault(true);


            LinkColumn(x => x.TrackingNumber)
             .OnClick(x => x.Go<Share.Shipment.ShipmentViewPage>()
             .Send("shipment", "item.ID"));


            CustomColumn("Tracking number excel")
                .VisibleIf("false")
                .ExportToExcel()
                .LabelText("Tracking number")
                .DisplayExpression("@item.TrackingNumber.ToString()")
                .ExcelTemplate(@"item.TrackingNumber.ToString()");

            Column(x => x.Date)
                .DisplayFormat("{0: d}");

            Column(x => x.ExpectedDate)
                .LabelText("Expected date of arrival/departure");
            //Column(x => x.PortOfArrival);
            Column(x => x.Route).DisplayExpression("@item.RouteName");

            Column(x => x.MyReferenceForCPInvoice)
                .LabelText("Customer Reference");

            Column(x => x.Company).LabelText("Company name")
                .DisplayExpression("@item.Company.Name");

            Column(x => x.VehicleNumber);
            Column(x => x.TrailerNumber);

            Column(x => x.Progress)
                .DisplayExpression("@item.Progress.ClientDisplay");

            LinkColumn("Consignments").HeaderText("Consignments")
                .Text(@"@await item.Consignments.Count()")
               .OnClick(x =>
               {
                   x.Go<Share.Consignments.ConsignmentsPage>()
                   .Send("shipment", "item.ID")
                   .SendReturnUrl();
               });


            Column(x => x.IsWeightsMismatch)
                .LabelText("Weights mismatch");

            ButtonColumn("Print")
                .HeaderText("Actions")
                .GridColumnCssClass("actions-merge")
                .VisibleIf("item.ProgressId == Progress.Draft")
                .OnClick(x =>
                {
                    x.Go<Share.Shipment.ShipmentView.PrintPage>()
                    .Target(OpenIn.NewBrowserWindow)
                    .Send("shipment", "item.ID");
                });

            ButtonColumn("Edit")
                .HeaderText("Actions")
                .GridColumnCssClass("actions-merge")
                .VisibleIf("item.IsEditableCustomer || item.IsCustomerAmendWithSingle")
                .Icon(FA.Edit)
                .OnClick(x => x.Go<Share.Shipment.ShipmentEnterPage>()
                .SendReturnUrl()
                .Send("item", "item.ID"));

            this.ArchiveButtonColumn("Shipment")
                .HeaderText("Actions")
                .GridColumnCssClass("actions-merge")
                .VisibleIf("!await item.Archive() || item.IsDeactivated");

            ButtonColumn("Archive")
               .VisibleIf("await item.Archive() && !item.IsDeactivated")
               .HeaderText("Actions")
               .GridColumnCssClass("actions-merge")
               .OnClick(x => x.PopUp<Share.Shipment.ShipmentArchiveConfirmationPage>()
               .Send("shipment", "item.ID"));

            ButtonColumn("Raise Support Ticket")
             .HeaderText("Actions")
             .GridColumnCssClass("actions-merge")
             .OnClick(x => x.PopUp<Admin.Dashboard.CustomerRiseSupportTicketPage>()
             .Send("shipment", "item.ID"));

            Button("Export")
                .Text("Export")
                .VisibleIf("CompanyUser.AccountsDepartment == true")
                .OnClick(x => x.Export(ExportFormat.Csv)
                    .Set(CommonActionParameter.ExportToCsv_FileName, "EAD Shipments"));

            Button("GB EORI Validator")
                .Text("GB EORI Validator")
                .OnClick(x => x.PopUp<Share.Shipment.EORIValidatorPage>());


            Button("Consignment Search")
            .OnClick(x => x.PopUp<Customer.ConsignmentIntoSearchEnterPage>()
                .Send("item", @"Request.Query[""ID""]")
                .SendReturnUrl());

            Button("New Shipment")
                .OnClick(x => x.Go<Share.Shipment.ShipmentEnterPage>()
                .SendReturnUrl())
                .Icon(FA.Plus);


            //search logic
            OnPostBound("set Cookie for url")
                .Code("await SearchUrlCookie.CreateCookieHistory(info.Date, info.DateMax, info.ExpectedDate, info.ExpectedDateMax,isNcts: false, isIntoUk: true);");

            OnControllerClassCode("set time")
                .Code(@"static void DateSet(vm.CustomerShipmentInList info,ConsignmentSearch consigmentSearch = null )
            {       
                    info.Date = consigmentSearch?.DateCons ?? null;
                    info.DateMax = consigmentSearch?.DateMaxCons ?? null;
                    info.ExpectedDate = consigmentSearch?.ExpectedDateCons ?? null;
                    info.ExpectedDateMax = consigmentSearch?.ExpectedDateMaxCons ?? null;
            }");
            OnControllerClassCode("set flag for search")
                .Code("bool isActiveConSearch = false;");

            OnBound("filter logic")
                .Code("await Filter(info);");

            OnControllerClassCode("filter logic")
                .Code(@"private async Task Filter(vm.CustomerShipmentInList info)
            {
                if (info.TrackingNumber.HasValue()) { DateSet(info); }
                if (info.MyReferenceForCPInvoice.HasValue()) { DateSet(info); }
                if (Request.Query[""Id""].ToString().HasValue())
                {
                    var consigmentSearch = await Database.Of<ConsignmentSearch>().Where(x => x.ID == new Guid(Request.Query[""Id""])).FirstOrDefault();

                    if (consigmentSearch != null && ((consigmentSearch?.ConsignmentNumber.HasValue() ?? false) || (consigmentSearch?.UCR.HasValue() ?? false))) { DateSet(info); isActiveConSearch = true; }
                }
                if (Request.IsGet() && Request.Lacks(""Date"") && !isActiveConSearch && (Request.Lacks(""ExpectedDate"") || Request.Lacks(""ExpectedDateMax"")))
                {
                    info.Date = LocalTime.Now.Add(TimeSpan.FromDays(-3)).Date;
                    info.DateMax = LocalTime.Now.Add(TimeSpan.FromDays(3)).Date;
                }
                if (info.ExpectedDate.HasValue || info.ExpectedDate.HasValue)
                {
                    info.Date = null;
                    info.DateMax = null;
                }
            }");

        }
    }
}

