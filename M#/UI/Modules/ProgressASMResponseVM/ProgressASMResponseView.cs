using MSharp;

namespace Modules
{
    public class ProgressASMResponseView : ViewModule<Domain.ProgressASMResponseVM>
    {
        public ProgressASMResponseView()
        {
            HeaderText(@"@item.Progress details");
            DataSource("info.Item = await ASMResponseService.GetResponseMessage(info.Consignment);");
            this.AddDependency(typeof(Domain.IASMResponseService), "ASMResponseService");

            Field(x => x.ASMMessage);
            Markup("<p>@item.ASMMessage.ToHtmlLines().Raw()</p>");

            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");


            Button("Close").OnClick(x => x.CloseModal());
        }
    }
}