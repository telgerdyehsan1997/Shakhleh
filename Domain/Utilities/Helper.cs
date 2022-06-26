using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Olive;
using Olive.Entities;
using System.Linq;
using System.Text.RegularExpressions;
using BarcodeLib;
using System.Drawing;
using System.IO;
using System.Text;

namespace Domain
{
    public class Helper
    {
        static IDatabase Database => Context.Current.Database();

        static Regex ValidEmailRegex = CreateValidEmailRegex();

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }
    }
}

