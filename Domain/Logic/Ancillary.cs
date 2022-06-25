namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;

    partial class Ancillary
    {
        public override async Task Validate()
        {
            await base.Validate();

            if (InsuranceCharge.HasValue && !IsInsuranceChargeValid())
                throw new ValidationException("The Insurance charge can only be set to a maximum of five decimal places.");

            await Task.CompletedTask;
        }

        /// <summary>
        /// Validates that the entered value is no more than by 2 decimal places.
        /// </summary>
        bool IsInsuranceChargeValid()
        {
            var number = InsuranceCharge.ToString();
            var length = number.Substring(number.IndexOf(".") + 1).Length;

            return length <= 5;
        }

        public override string ToString(string format)
        {
            if (format == "F")
                return new[] { Country.ToStringOrEmpty(), FreightChargePerTonne.ToStringOrEmpty(), FreightChargePerTonne.ToStringOrEmpty(), ValueForVAT.ToStringOrEmpty() }.ExceptNull().ToString(" ");
            return base.ToString(format);
        }
    }

}