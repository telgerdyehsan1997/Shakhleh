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
        private const int Width = 300;
        public static async Task<IEnumerable<Currency>> GetCurrencyList(Shipment shipment = null)
        {
            var currencyList = (await Database.Of<Currency>().Where(c => c.IsDeactivated == false).OrderBy(x => x.Name).GetList()).ToList();
            if (shipment != null && shipment.IsOutUK)
            {
                var currencies = await Database.Of<ExchangeRate>()
                          .Where(t => t.From <= shipment.Date.Date && t.To >= shipment.Date.Date)
                          .GetList().Select(t => t.Currency);

                currencyList = currencyList.Intersect(currencies).ToList();
                var gbp = await Currency.FindByName("GBP");
                if (currencyList.Lacks(gbp))
                    currencyList.Add(gbp);
            }

            var result = currencyList.FindAll(t => t.Name.IsAnyOf("GBP", "EUR")).OrderByDescending(t => t.Name).ToList();
            currencyList.RemoveAll(t => t.Name.IsAnyOf("GBP", "EUR"));

            result.AddRange(currencyList);
            return result;
        }

        public static async Task<IEnumerable<Currency>> GetCurrencyAllList()
        {
            var currencyList = (await Database.Of<Currency>().Where(c => c.IsDeactivated == false).OrderBy(x => x.Name).GetList()).ToList();
            var result = currencyList.FindAll(t => t.Name.IsAnyOf("GBP", "EUR")).OrderByDescending(t => t.Name).ToList();
            currencyList.RemoveAll(t => t.Name.IsAnyOf("GBP", "EUR"));

            result.AddRange(currencyList);
            return result;
        }

        public static Task<IEnumerable<Route>> GetAvailableRoutes(Port port)
        {
            return Database.Of<Route>().Where(x => x.UKPortId == port && x.IsDeactivated == false).OrderBy(u => u.UKPort).GetList();
        }

        public static async Task<IEnumerable<CPC>> GetSpecialCPCs(Company company, Company trader)
        {
            var companiesCPCs = await company.SpecialCPCs.GetList();
            var traiderCPCs = trader != null ? await trader.SpecialCPCs.GetList() : Enumerable.Empty<CompanySpecialCPCLink>();
            return companiesCPCs.Concat(traiderCPCs).Except(t => t.IsDeactivated).Distinct().Select(t => t.CPC);
        }

        public static IEnumerable<Progress> Transmitables => new List<Progress>
            {
                Progress.ReadyToTransmitAPI,
                Progress.ReadyToTransmit,
                Progress.ManualCPC,
                Progress.ManualGenereal,
                Progress.ManualLicense,
                Progress.ManualRoute,
                Progress.ManualQuota
            };

        public static IEnumerable<Progress> Removable => new List<Progress>
        {
            Progress.Draft,
            Progress.ReadyToTransmit,
            Progress.Cancelled
        };

        public static IEnumerable<Progress> AdminEditableShipment => new List<Progress>
        {
            Progress.Draft,
            Progress.AwaitingArrival,
            Progress.AwaitingDeparture,
            Progress.ProcessingErrorArrival,
            Progress.ProcessingErrorDeparture,
            Progress.ASMReject
        };

        public static IEnumerable<Progress> AdminEditShipment => new List<Progress>
        {
            Progress.DutyPayment,
            Progress.InternalError,
            Progress.ManualCPC,
            Progress.ManualCPCASMAccepted,
            Progress.ManualCPCASMRejected,
            Progress.ManualGenereal,
            Progress.ManualGenerealASMAccepted,
            Progress.ManualGenerealASMRejected,
            Progress.ManualLicense,
            Progress.ManualLicenseASMAccepted,
            Progress.ManualLicenseASMRejected,

            Progress.ManualQuota,
            Progress.ManualRoute,
            Progress.ManualRouteASMAccepted,
            Progress.ManualRouteASMRejected,

            Progress.QueriedArrived,

            Progress.QueriedWithCustoms,
            Progress.WithCustoms,

        };

        public static IEnumerable<Progress> PublicEditableShipment => new List<Progress>
        {
            Progress.Draft,
            Progress.AwaitingDeparture,
            Progress.AwaitingArrival,
            //Progress.DutyPayment
        };

        public static IEnumerable<Progress> AdminEditableConsignments => new List<Progress>
        {
            Progress.Draft,
            Progress.ProcessingErrorArrival,
            Progress.ProcessingErrorDeparture,
            Progress.InternalError,
            Progress.ASMReject,

            Progress.ManualCPC,
            Progress.ManualCPCASMRejected,
            Progress.ManualCPCASMAccepted,

            Progress.ManualGenereal,
            Progress.ManualGenerealASMRejected,
            Progress.ManualGenerealASMAccepted,

            Progress.ManualLicense,
            Progress.ManualLicenseASMRejected,
            Progress.ManualLicenseASMAccepted,

            Progress.ManualQuota,

            Progress.ManualRoute,
            Progress.ManualRouteASMRejected,
            Progress.ManualRouteASMAccepted,

            Progress.QueriedWithCustoms,
            Progress.QueriedArrived,
            Progress.DutyPayment,

            Progress.WithCustoms,

        };

        public static IEnumerable<Progress> PublicEditableConsignments => new List<Progress>
        {
            Progress.Draft,
            Progress.AwaitingArrival,
            Progress.AwaitingDeparture,
            Progress.DutyPayment
        };

        public static IEnumerable<Progress> Transmit => new List<Progress>
            {
                           Progress.ASMAccept,
                           Progress.ASMReject,
                           Progress.AwaitingArrival,
                           Progress.AwaitingDeparture,
                           Progress.Draft,
                           Progress.DutyPayment,
                           Progress.ManualCPC,
                           Progress.ManualCPCASMRejected,
                           Progress.ManualGenereal,
                           Progress.ManualGenerealASMAccepted,
                           Progress.ManualLicenseASMRejected,
                           Progress.ManualLicense,
                           Progress.ManualLicenseASMAccepted,
                           Progress.ManualLicenseASMRejected,
                           Progress.ManualRoute,
                           Progress.ManualRouteASMAccepted,
                           Progress.ManualRouteASMRejected,
                           Progress.QueriedArrived,
                           Progress.ReadyToTransmitAPI,
                           Progress.ReadyToTransmit,

            };

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        public static bool EmailIsValid(string emailAddress)
        {
            return ValidEmailRegex.IsMatch(emailAddress);
        }

        public static IEnumerable<Progress> LogShipmentStatus => new List<Progress>
            {
                Progress.ManualCPC,
                Progress.ASMReject,
                Progress.EntryControlled,
                Progress.ManualCPCASMAccepted,
                Progress.ManualCPCASMRejected,
                Progress.ManualGenereal,
                Progress.ManualGenerealASMAccepted,
                Progress.ManualGenerealASMRejected,
                Progress.ManualLicenseASMAccepted,
                Progress.ManualLicenseASMRejected,
                Progress.ManualLicense,
                Progress.ManualQuota,
                Progress.ManualRoute,
                Progress.ManualRouteASMAccepted,
                Progress.ManualRouteASMRejected,
                Progress.QueriedWithCustoms,
                Progress.WithCustoms,

            };

        public static string BarcodeGenerator(string mrn)
        {
            var newMrn = "";
            if (mrn.Contains("/"))
                newMrn = mrn.Split("/")[0];

            using (var barcode = new Barcode())
            {

                barcode.IncludeLabel = true;
                barcode.Alignment = AlignmentPositions.CENTER;
                barcode.Width = Width;
                barcode.Height = 100;
                barcode.RotateFlipType = RotateFlipType.RotateNoneFlipNone;
                barcode.BackColor = Color.White;
                barcode.ForeColor = Color.Black;

                barcode.Encode(TYPE.CODE128B, mrn);
                var path = $"BarCode-{newMrn}.jpg";
                barcode.SaveImage(path, SaveTypes.JPG);
                barcode.IncludeLabel = true;
                return "/" + path;
            }
        }
        public static string UniqueId()
        {
            var builder = new StringBuilder();
            Enumerable
                .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(10)
                .ToList().ForEach(e => builder.Append(e));

            return builder.ToString().ToUpper();
        }

        public static Task<int> TotalCountUnseenResponse()
        {
            return Database.Of<UserReponseNotification>().Where(x => !x.HasSeen && x.UserId == Context.Current.User().ExtractUser<User>() && x.Response.IsConfirm).Count();
        }

        public static async Task<IEnumerable<UserReponseNotification>> TotalUnseenResponse(SupportTicket supportTicket)
        {
            return await Database
                .Of<UserReponseNotification>()
                .Where(x => !x.HasSeen && x.UserId == Context.Current.User().ExtractUser<User>() && x.Response.IsConfirm && x.Response.SupportTicketId == supportTicket)
                .GetList()
                .ToList();
        }

        public static Task<bool> HasBeenHighlighted(SupportTicket supportTicket)
        {
            return Database.
                Of<UserReponseNotification>()
                .Any(x => !x.HasSeen && x.UserId == Context.Current.User().ExtractUser<User>() && x.Response.IsConfirm && x.Response.SupportTicketId == supportTicket);
        }

    }
}

