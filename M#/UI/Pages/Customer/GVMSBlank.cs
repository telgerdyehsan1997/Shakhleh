using MSharp;
using Domain;
using Modules;

namespace Customer
{
    public class GVMSBlankPage : RootPage
    {
        public GVMSBlankPage()
        {
            Add<Modules.GVMSBlankList>();

        }
    }
}