namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Security;
    using Olive.Entities;
    using System.Text.RegularExpressions;

    /// <summary> 
    /// Provides the business logic for Port class.
    /// </summary>
    partial class Port
    {
        public bool IsGBDoubleO => TransitOffice.NCTSCode == Constants.GBDoubleO;

        protected override async Task OnValidating(EventArgs e)
        {
            await base.OnValidating(e);

            if (!PortCode.All(c => c.IsLetter()))
                throw new ValidationException("The Port code can only contain letters.");

            if (TitledLocationCode.HasValue())
                if (!TitledLocationCode.All(c => c.IsLetter()))
                    throw new ValidationException("Location Code can only contain letters.");

            if (TitledUsePortCode.HasValue())
                if (!TitledUsePortCode.All(c => c.IsLetter()))
                    throw new ValidationException("Use Port Code can only contain letters.");

            if (DTIBadge.HasValue())
                if (DTIBadge.Any(char.IsDigit))
                    throw new ValidationException("DTI Badge must only contain letters.");
        }

        public static Task<Port> GetActivePortByName(string portName)
        {
            var port = Database.FirstOrDefault<Port>(x => x.PortName == portName && !x.IsDeactivated);
            if (port == null) { throw new ValidationException("Invalid Port or does not exist."); }
            return port;
        }

        public static Task<Port> GetActivePortByCode(string portCode)
        {
            var port = Database.FirstOrDefault<Port>(x => x.PortCode == portCode && !x.IsDeactivated);
            if (port == null) { throw new ValidationException("Invalid Port or does not exist."); }
            return port;
        }
        public static Task<Port> GetPortByName(string portName, bool isUk)
        {
            var port = Database.FirstOrDefault<Port>(x => x.PortName == portName && x.Non_UK == !isUk);
            if (port == null) { throw new ValidationException("Invalid Port or does not exist."); }
            return port;
        }

    }
}
