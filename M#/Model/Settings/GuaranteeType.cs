using MSharp;

namespace Domain
{
    class CompanyGuaranteeType : EntityType
    {
        public CompanyGuaranteeType()
        {
            InstanceAccessors("None", "Own", "Different company guarantee");
            String("Name");
        }
    }
}