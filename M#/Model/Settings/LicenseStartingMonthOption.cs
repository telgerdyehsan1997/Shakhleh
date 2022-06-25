using MSharp;

namespace Domain
{
    class LicenseStartingMonthOption : EntityType
    {
        public LicenseStartingMonthOption()
        {
            IsEnumReference();
            String("Name").Mandatory();
            Int("Month number").Mandatory();
        }
    }
}