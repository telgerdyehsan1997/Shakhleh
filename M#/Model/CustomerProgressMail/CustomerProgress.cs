using MSharp;

namespace Domain
{
    class CustomerProgress : EntityType
    {
        public CustomerProgress()
        {
            Associate<Progress>("Progress").DatabaseIndex();
            Associate<Company>("User").DatabaseIndex();
            Bool("Recieve email notification user customer").Mandatory();

            Bool("Do not recieve email notification customer").Mandatory();
        }
    }
}