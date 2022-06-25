namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using Domain.AEB.DTOs;

    partial class Contact
    {
        public override async Task Validate()
        {
            await base.Validate();

            FirstName = FirstName.CapitaliseFirstLetters();
            LastName = LastName.CapitaliseFirstLetters();

            if (await IsDuplicate() && IsNew)
                throw new ValidationException(Name + " is already taken .please try with new one.");
        }

        private async Task<bool> IsDuplicate()
        {
            return (await Database.Of<Person>().Where(x => x.FirstName == FirstName && x.LastName == LastName).GetList()).HasMany();
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
    }
}