using MSharp;

namespace Modules
{
    class BannerMessageList : BaseListModule<Domain.BannerMessage>
    {
        public BannerMessageList()
        {
            HeaderText("Banner Messages");

            this.ArchiveSearch()
                .DefaultValueExpression("false");

            SearchButton("Search")
                .Icon(FA.Search)
                .OnClick(x => x.ReturnView());


            //for customize row its will always hidden 
            Column(x => x.Message);


            ButtonColumn("Edit").HeaderText("Edit")
               .Icon(FA.Edit)
               .OnClick(x => x.Go<Admin.Dashboard.BannerMessageFormPage>()
               .Send("item", "item.ID")
               .SendReturnUrl());


            this.ArchiveButtonColumn("BannerMessage")
              .HeaderText("Delete");

            Button("Banner Message")
              .IsDefault()
              .Icon(FA.Plus)
              .CssClass("float-right")
              .OnClick(x =>
              {
                  x.Go<Admin.Dashboard.BannerMessageFormPage>()
                  .SendReturnUrl();
              });


        }
    }
}