using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using MSharp;
using Olive;
using Olive.Entities;

public static class Helper
{
    public static ListButton<T> ArchiveButtonColumn<T>(this ListModule<T> listModule, string itemName = "item") where T : GuidEntity
    {
        listModule.RowCssClass("c#:item.IsDeactivated ? \"row-archived\":\"\"");
        return listModule
            .ButtonColumn("@(item.IsDeactivated ? \"Unarchive\":\"Archive\")")
            .HeaderText("Archive")
            .CssClass("c#:item.IsDeactivated ? \"row-archived\":\"\"")
            .GridColumnCssClass("actions-sep")       
            .Icon(FA.Archive)
            .OnClick(x =>
            {
                x.PopUp<Share.Archive.ArchivePopUpPage>().Send("entity", "item.ID");              
            });
    }



    public static PropertyFilterElement<T> ArchiveSearch<T>(this ListModule<T> listModule) where T : GuidEntity, Domain.IArchivable
    {
        return listModule.Search(x => (x as Domain.IArchivable).IsDeactivated).Label("Status").Control(ControlType.HorizontalRadioButtons).DefaultValueExpression("false");
    }

    public static PropertyFilterElement<Domain.Shipment> AddAdminProgressFilter(this ListModule<Domain.Shipment> @this)
    {
        return @this.Search(x => x.Progress).Label("Progress")
            .AsCollapsibleCheckBoxList()
            .DataSource("Database.GetList<Progress>().ExceptNull().Distinct(x => x.AdminDisplay)")
            .MemoryFilterCode(@"if (info.Progress.Any())
            {
                result = result.Where(x => x.Progress.AdminDisplay.IsAnyOf(info.Progress.Select(p => p.AdminDisplay)));
            }")
            .DisplayExpression("item.AdminDisplay");
    }
    public static PropertyFilterElement<Domain.Shipment> AddClientProgressFilter(this ListModule<Domain.Shipment> @this)
    {
        return @this.Search(x => x.Progress).Label("Progress")
            .AsCollapsibleCheckBoxList()
            .DataSource("Database.GetList<Progress>().ExceptNull().Distinct(x => x.ClientDisplay)")
            .MemoryFilterCode(@"if (info.Progress.Any())
            {
                result = result.Where(x => x.Progress.ClientDisplay.IsAnyOf(info.Progress.Select(p => p.ClientDisplay)));
            }")
            .DisplayExpression("item.ClientDisplay");
    }

    public static TModule AddDependency<TModule>(this TModule module, Type serviceType, string propertyName = null)
    where TModule : Module
    {
        var servicePropertyName = propertyName.Or(serviceType.Name.Substring(1));
        module.Inject($"{serviceType.Namespace}.{serviceType.Name}").Name(servicePropertyName);

        return module;
    }

    public static Module AddDependency<TServiceType>(this Module module, string propertyName = null)
    {
        var serviceType = typeof(TServiceType);
        if (!serviceType.IsInterface)
            throw new ArgumentException("Generic type must be an interface.");
        var servicePropertyName = propertyName.Or(serviceType.Name.Substring(1));
        module.Inject($"{serviceType.Namespace}.{serviceType.Name}").Name(servicePropertyName);

        return module;
    }

    public static Workflow RunInTransaction(this Workflow workflow, Action actions, string scopeVariableName = "scope")
    {
        workflow.CSharp($@"
        using(var {scopeVariableName} = Database.CreateTransactionScope())
        {{");

        actions.Invoke();

        workflow.CSharp($@"
            {scopeVariableName}.Complete();
        }}");

        return workflow;
    }
}
