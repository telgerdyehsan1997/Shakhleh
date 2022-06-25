namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using Domain.AEB.DTOs;
    partial class Person
    {
        virtual internal PersonDTO ToDTO()
        {
            return new PersonDTO
            {
                Forename = FirstName,
                Surname = LastName,
                Email = Email,
                Phone = "",
                Supplement = "",
                Initials = "",
                IsDeactivated = false
            };
        }
    }
}
