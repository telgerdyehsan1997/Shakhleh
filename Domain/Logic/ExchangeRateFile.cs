namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using Olive.Csv;
    using System.Data;
    using System.Security.Principal;

    partial class ExchangeRateFile
    {

        public bool IsAttachmentVisibleTo(IPrincipal user) => true;

        public bool IsFileVisibleTo(IPrincipal user) => true;

        protected override async Task OnSaved(SaveEventArgs e)
        {
           // await ReadExchangeRates(this.File);

            await base.OnSaved(e);
        }

        //private async Task ReadExchangeRates(Blob file)
        //{
        //    var importFile = CsvReader.Read(await file.GetContentTextAsync(), isFirstRowHeaders: true);

        //    if (!(await ValidateColumns(importFile.Columns)))
        //        throw new Exception("Header does not match.");

        //    var rows = importFile.GetRows().Skip(1);

        //    await rows.DoAsync(async (row, index) => await ImportRow(row));
        //}


        //async Task<bool> ValidateColumns(DataColumnCollection columns)
        //{
        //    var columnNames = columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

        //    var names = new List<string> {
        //        "Country/Territories",
        //        "Currency",
        //        "Currency Code",
        //    };

        //    return columnNames.Any(t => names.Contains(t));
        //}

        //async Task ImportRow(DataRow row)
        //{
        //    try
        //    {
        //        var country = row[0].ToString().Trim();
        //        var currencyCode = row[2].ToString().Trim();
        //        var rate = row[3].ToString().To<decimal>();
        //        var startDate = row[4].ToString().To<DateTime>().Date;
        //        var endDate = row[5].ToString().To<DateTime>().Date;

        //        var currency = await Currency.FindByName(currencyCode);
        //        if (currency == null) throw new Exception($"Currency Code {currencyCode} is not defined.");

        //        var exchangeRate = await ExchangeRate.FindByCurrencyAndFromAndTo(currency, startDate, endDate);
        //        if (exchangeRate != null)
        //        {
        //            exchangeRate = exchangeRate.Clone();
        //            exchangeRate.Rate = rate;
        //            exchangeRate.File = this;
        //        }
        //        else
        //            exchangeRate = new ExchangeRate
        //            {
        //                Country_Territory = country,
        //                Currency = currency,
        //                File = this,
        //                From = startDate,
        //                To = endDate,
        //                Rate = rate,
        //            };
        //        await Database.Save(exchangeRate);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.For<ExchangeRate>().Error(ex);
        //    }
        //}
    }
}