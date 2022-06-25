using MSharp;
using System;
using System.Collections.Generic;

namespace Modules
{
    class GVMSTransmissionLogList : BaseListModule<Domain.Shipment>
    {
        public GVMSTransmissionLogList()
        {
            RootCssClass("mfa");

            HeaderText("GVMS Transmission Log").SortingStatement("item.TrackingNumber")
                .DataSource(@"await Shipment.GetGVMSShipments(info.Type ,info.Company, info.ExpectedDate,info.ExpectedDateMax, Request.Query[""Id""])");             

            Search(x => x.TrackingNumber);

            Search(x => x.Type).Control(ControlType.HorizontalRadioButtons)
                .DataSource("Database.GetList<ShipmentType>()");

            Search(x => x.Route).AsAutoComplete()
                .DataSource("Database.GetList<Route>(x => !x.IsDeactivated)")
                .MemoryFilterCode("result = result;");

            Search(x => x.Company).Label("Company name").AsAutoComplete()
               .DataSource("Database.GetList<Company>(x => !x.IsDeactivated)")
               .MemoryFilterCode("result = result;");

            Search(x => x.IsNCTSShipmentOutConvertible).Label("Has NCTS").Control(ControlType.HorizontalRadioButtons);

            Search(x => x.Date).Label("Date created")
               .DefaultValueExpression("LocalTime.Now.Add(TimeSpan.FromDays(-3)).Date; info.DateMax = LocalTime.Now.Add(TimeSpan.FromDays(3)).Date");

            Search(x => x.ExpectedDate).Label("Expected date of arrival/departure")
              .DefaultValueExpression("LocalTime.Now.Add(TimeSpan.FromDays(-3)).Date; info.ExpectedDateMax = LocalTime.Now.Add(TimeSpan.FromDays(3)).Date");

            Search(x => x.MyReferenceForCPInvoice).Label("Customer Reference");

            Search(x => x.VehicleNumber);
            Search(x => x.TrailerNumber);

            this.AddAdminProgressFilter();

            Search(x => x.GVMSStatus)
                .DataSource("Database.GetList<GVMSStatus>()")              
                .AsCollapsibleCheckBoxList();

            SearchButton("Search").Icon(FA.Search).OnClick(x => { x.ReturnView(); }).IsDefault(true);

            LinkColumn(x => x.TrackingNumber).OnClick(x => x.Go<Share.Shipment.ShipmentViewPage>().Send("shipment", "item.ID"));
            Column(x => x.Date);
            Column(x => x.Type);
            Column(x => x.ExpectedDate)
                .HeaderTemplate(cs("(\"Expected date of arrival\".OnlyWhen(info.Type == ShipmentType.IntoUk).Or(\"Expected date of departure\".OnlyWhen(info.Type == ShipmentType.OutOfUk)).Or(\"Expected date of arrival/departure\"))"));
            Column(x => x.Route);
            Column(x => x.MyReferenceForCPInvoice)
                .LabelText("Customer Reference");
            Column(x => x.Company).LabelText("Company name").DisplayExpression("@item.Company.Name");
            Column(x => x.VehicleNumber);
            Column(x => x.TrailerNumber);
            Column(x => x.Progress.AdminDisplay).LabelText("Progress");           
            Column(x => x.IsNCTSShipmentOutConvertible)
                .LabelText("Has NCTS");
            Column(x => x.GVMSStatus);
            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit).VisibleIf("item.IsEditable")
                .OnClick(x => x.Go<Share.Shipment.ShipmentEnterPage>().Send("item", "item.ID").SendReturnUrl());                   
        }
    }
}

