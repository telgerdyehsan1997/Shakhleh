using MSharp;

namespace App
{
    public class Project : MSharp.Project
    {
        public Project()
        {
            Name("channel.ports").SolutionFile("channel.ports.sln");

            DefaultDateFormat("{0:d}");
            DefaultDateTimeFormat("{0:g}");

            Role("Local.Request");
            Role("Anonymous");
            Role("Admin").SkipQueryStringSecurity();
            Role("Super Admin").SkipQueryStringSecurity();
            Role("Customer");

            Layout("Front end").AjaxRedirect().Default().VirtualPath("~/Views/Layouts/FrontEnd.cshtml");
            Layout("Blank").AjaxRedirect().VirtualPath("~/Views/Layouts/Blank.cshtml");
            Layout("Front end Modal").Modal().VirtualPath("~/Views/Layouts/FrontEnd.Modal.cshtml");
            Layout("Print").AjaxRedirect().VirtualPath("~/Views/Layouts/Print.cshtml");

            PageSetting("LeftMenu");
            PageSetting("SubMenu");
            PageSetting("TopMenu");
            PageSetting("HeaderModule");
            PageSetting("SubHeaderModule");


            StyleRequiredFormElements(true);


        }
    }
}