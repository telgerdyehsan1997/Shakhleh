using MSharp;

namespace Admin
{
    class GVMSTransmissionLogPage : SubPage<AdminPage>
    {
        public GVMSTransmissionLogPage()
        {
            BrowserTitle("GVMS Transmission Log");
            Add<Modules.GVMSTransmissionLogList>();
            //BaseController("MFABaseController");
            //OnStart(x =>
            //{
            //    x.CSharp(@"var url = await SearchUrlCookie.GetUrl(isNcts: false, isIntoUk: true);");
            //    x.If("!(Request.IsAjaxGet() && Request.QueryString.HasValue) &&  Request.IsGet() && url.HasValue() && Request.ToRawUrl() != url")
            //    .CSharp("return Redirect(url);");
            //});
        }
    }
}