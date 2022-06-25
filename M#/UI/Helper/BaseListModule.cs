using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MSharp;
using Olive.Entities;

namespace Modules
{
    public abstract class BaseListModule<TEntity> : ListModule<TEntity> where TEntity : IEntity
    {
        protected BaseListModule()
        {
            RenderMode(ListRenderMode.ResponsiveGrid);
            PageSize(10);
            Sortable();
            ShowHeaderRow();
        }
    }
}
