namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using System.Security.Principal;

    partial class TransitOfficeFile
    {
        protected override async Task OnSaved(SaveEventArgs e)
        {
            await base.OnSaved(e);
            await SaveLines();
        }

        public async Task SaveLines()
        {
            var content = await File.GetContentTextAsync();
            var lines = content.ToLines().ExceptNull().ToList();
            var errors = new List<TransitOfficeFileError>();
            var failedCounter = 0;

            lines = lines.Except(t => t.IsEmpty()).ToList();
            var offices = await Database.GetList<TransitOffice>().ToList();
            var mustRemoveOffices = offices.Where(t => lines.None(x => x.Split(",")[2] == t.NCTSCode)).ToList();

            foreach (var mustRemoveOffice in mustRemoveOffices)
            {
                if (!mustRemoveOffice.IsDeactivated)
                    await Database.Update(mustRemoveOffice, t => t.IsDeactivated = true);
            }

            for (var row = 1; row < lines.Count(); row++)
            {
                var cols = lines[row].Split(",");

                try
                {
                    var countryCode = cols[0];
                    var countryName = cols[1];
                    var nctsCode = cols[2];
                    var name = cols[3];
                    var isDeparture = cols[4].HasValue();
                    var isDestination = cols[5].HasValue();
                    var isTransit = cols[6].HasValue();
                    var nearestOffice = cols[7];
                    var alias = cols[8];

                    var existingOffice = offices.FirstOrDefault(x => x.NCTSCode == nctsCode);

                    if (existingOffice != null)
                    {
                        await Database.Update(existingOffice, x =>
                        {
                            x.CountryCode = countryCode;
                            x.CountryName = countryName;
                            x.UsualName = name;
                            x.Departure = isDeparture;
                            x.Destination = isDestination;
                            x.Transit = isTransit;
                            x.NearestOffice = nearestOffice;
                            x.IsDeactivated = false;
                        });
                        if (alias.HasValue())
                        {
                            var transitOfficeAlias = await existingOffice.TransitOfficeAlias.FirstOrDefault();
                            if (transitOfficeAlias != null)
                                await Database.Update(transitOfficeAlias, t => t.Alias = alias);
                            else
                                await Database.Save(new TransitOfficeAlias { TransitOffice = existingOffice, Alias = alias });
                        }
                    }
                    else
                    {
                        var transitOffice = await Database.Save(new TransitOffice
                        {
                            CountryCode = countryCode,
                            CountryName = countryName,
                            UsualName = name,
                            NCTSCode = nctsCode,
                            Departure = isDeparture,
                            Destination = isDestination,
                            Transit = isTransit,
                            NearestOffice = nearestOffice,
                        });
                        if (alias.HasValue())
                            await Database.Save(new TransitOfficeAlias { TransitOffice = transitOffice, Alias = alias });
                    }
                }
                catch (Exception ex)
                {
                    failedCounter++;
                    await Database.Save(new TransitOfficeFileError { ErrorReason = ex.Message, RowNumber = row, TransitOfficeFile = this });
                    continue;
                }
            }


            var status = TransitOfficeFileStatus.PartialSuccess;
            if (failedCounter == lines.Count() - 1)
                status = TransitOfficeFileStatus.Failed;
            else if (failedCounter == 0)
                status = TransitOfficeFileStatus.Successful;

            await Database.Update(this, t => t.Status = status, SaveBehaviour.BypassSaved);
        }

        public bool IsFileVisibleTo(IPrincipal user) => true;


    }
}