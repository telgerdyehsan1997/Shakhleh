using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public partial class ReportErrorLog
    {

        public override string ToString(string format)
        {
            if (format == "F")
                return $"{FileName} {Error}";

            return base.ToString(format);
        }

    }
}
