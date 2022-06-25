using System;
using System.Collections.Generic;
using System.Text;
using Olive;

namespace Domain
{
    public class AsmFileHelper
    {
        public static bool IsX2(string fileName)
        {
            var fileNameParts = fileName.Split('-');
            var code = fileNameParts.Length > 1 ? fileNameParts[1] : null;
            return code.ToStringOrEmpty().ToUpper() == "X2";
        }
    }
}
