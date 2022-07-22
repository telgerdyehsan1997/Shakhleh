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
            .ButtonColumn("@(item.IsDeactivated ? \"بیرون آوردن از آرشیو\":\"آرشیو کردن\")")
            .HeaderText("آرشیو")
            .CssClass("c#:item.IsDeactivated ? \"row-archived\":\"\"")
            .GridColumnCssClass("actions-sep")
            .Icon(FA.Archive)
            .OnClick(x =>
            {
                x.CSharp("await item.ToggleArchive();");
                x.Reload();
            });
    }
    public static PropertyFilterElement<T> ArchiveSearch<T>(this ListModule<T> listModule) where T : GuidEntity, Domain.IArchivable
    {
        return listModule.Search(x => (x as Domain.IArchivable).IsDeactivated)
            .Label("وضعیت").Control(ControlType.HorizontalRadioButtons).DefaultValueExpression("false");
    }
}
