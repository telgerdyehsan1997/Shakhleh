using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Olive;

namespace Domain
{
    public static class Validator
    {
        public static bool IsValidNCTS(string ncts)
        {
            // first two characters alpha ,the last 6 characters alphanumeric
            const int lenght = 8;
            if (ncts.Length != lenght) return false;
            return Regex.IsMatch(ncts, @"^[a-zA-Z{2}][a-zA-Z0-9{6}]*$");
        }

        public static bool IsValidVehicleNumber(string vehicleNo)
        {
            if (vehicleNo.IsEmpty()) return true;
            return Regex.IsMatch(vehicleNo, @"^(\d|\w)+$");
        }

        public static bool IsValidMobileNo(string mobileNo)
        {
            if (mobileNo.IsEmpty()) return true;
            return Regex.IsMatch(mobileNo, @"^[0-9]*$");
        }

        public static bool IsValidCustomerReference(string reference)
        {
            if (reference.IsEmpty()) return true;
            return Regex.IsMatch(reference, @"^[a-zA-Z0-9]*$");
        }
    }
}
