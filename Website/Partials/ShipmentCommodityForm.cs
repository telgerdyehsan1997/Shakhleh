using Domain;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Controllers
{
    partial class CommoditiesCommodiyEnterController
    {
        public async Task<bool> IsPreferenceTypeVisible(vm.CommodityDetails info)
            => await IsCountryUsingPreference(info) && info.Consignment.Shipment.IsInUK;

        public Task<IEnumerable<PreferenceType>> GetPreferenceTypes()
        {
            return Database.Of<PreferenceType>().Where(x => x.Name != "Statement of Origin (Importers Knowledge)").GetList();
        }

        public async Task<bool> IsPreferenceCertificateNumberVisible(vm.CommodityDetails info)
        {
            return await IsCountryUsingPreference(info) &&
            (info.PreferenceType != null && info.PreferenceType == PreferenceType.PreferenceCertificateNumber);
        }

        async Task<bool> IsCountryUsingPreference(vm.CommodityDetails info)
        {
            var result = false;
            result = info.CountryOfDestination != null;

            if (result)
                result = info.CountryOfDestination.PreferenceAvailable == true;

            if (result)
                result = info?.HasPreference == true;

            return result;
        }
    }
}
