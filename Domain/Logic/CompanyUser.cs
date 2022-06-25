namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using Domain.AEB.DTOs;

    partial class CompanyUser
    {
        public override IEnumerable<string> GetRoles()
        {
            return base.GetRoles().Concat("Customer");
        }

        public override Task Validate()
        {
            base.Validate();

            FirstName = FirstName.CapitaliseFirstLetters();
            LastName = LastName.CapitaliseFirstLetters();

            return Task.CompletedTask;
        }

        override internal PersonDTO ToDTO()
        {
            return new PersonDTO
            {
                Forename = FirstName,
                Surname = LastName,
                Email = Email,
                Phone = TelephoneNumber,
                Supplement = "",
                Initials = "",
                IsDeactivated = IsDeactivated
            };
        }

        public bool IsEAD => Company.CanDoEAD().GetAwaiter().GetResult();
    }
}