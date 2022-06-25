using MSharp;
using Domain;

namespace Modules
{
    public class Header : GenericModule
    {
        public Header()
        {
            IsInUse().IsViewComponent().WrapInForm(false);

            var logo = Image("Logo").CssClass("logo").ImageUrl("~/img/Logo.png")
                  .OnClick(x => x.Go("~/"));

            var burger = Link("Burger")
                .NoText()
                .CssClass("navbar-toggler collapsed")
                .Icon(FA.Bars)
                .ExtraTagAttributes("type=\"button\" data-toggle=\"collapse\" " +
                "data-target=\".navbar-collapse\" aria-expanded=\"false\" " +
                "aria-controls=\".navbar-collapse\" aria-label=\"Toggle navigation\"");

            Markup($@"
                <nav class=""navbar navbar-expand-md navbar-inverse sticky-top"">
                          {logo.Ref}
                          {burger.Ref}
                     <div class=""collapse navbar-collapse"">
                        @if (User.IsInRole(""Undischarged""))
                        {{
                             @(await Component.InvokeAsync<UndischargedMainMenu>())
                        }}
                        else {{
                            @(await Component.InvokeAsync<MainMenu>())
                        }}
                     </div>
                </nav>
                @{{var data = await Database.Of<BannerMessage>().Where(x => !x.IsDeactivated).GetList().Select(x => x.Message);}}
                   @if(data.Count() > 0)
                    {{
                       <div class=""tickerwrapper"">
                            <ul class=""list"">
                             @foreach (var item in data)
                            {{
                               <li class='listitem'>
                                    <span> @item</span>
                                </li>
                            }}
                         </ul>
                       </div>
                  }}");
           
            Reference<MainMenu>();
        }
    }
}