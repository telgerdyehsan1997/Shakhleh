namespace Domain
{
    using System;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;

  
    partial class HealthCertificate
    {
        public override async Task Validate()
        {
            await base.Validate();

            if (Description.HasValue() && Code.IsEmpty())
                throw new ValidationException("Health Certificate Code must be populated.");

        }
    }
}