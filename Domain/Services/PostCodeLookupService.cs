namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Entities.Data;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Net;

    public class PostCodeLookupService
    {
        IDatabase Database;
        readonly string PostCodePattern = @"([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})";

        public PostCodeLookupService(IDatabase database)
        {
            Database = database;
        }

        public async Task<string> FindAddresses(string postCode)
        {
            if (postCode.IsEmpty() || !Regex.IsMatch(postCode, PostCodePattern))
                return @"{ ""result"": [], ""error"": ""Please provide full postcode."" }";

            using (var @lock = await new AsyncLock().Lock())
            {
                postCode = postCode.Remove(" ").Trim();

                if (postCode.IsEmpty())
                    return string.Empty;

                var cachedPostCode = await Database.Of<PostCodeHistory>().Where(x => x.PostCode == postCode).FirstOrDefault();

                if (cachedPostCode != null)
                {
                    if (cachedPostCode.AddedOn < LocalTime.Now.AddDays(-1))
                        await Database.Delete(cachedPostCode);
                    else
                        return cachedPostCode.Result;
                }

                var apiKey = Config.Get<string>("PostCodeLookup:ApiKey");
                var apiUri = Config.Get<string>("PostCodeLookup:ApiUri");
                var isTestMode = Config.Get<bool>("PostCodeLookup:TestMode");
                var testPostCode = Config.Get<string>("PostCodeLookup:TestPostCode");
                var requestUrl = apiUri.Replace("{postcode}", isTestMode ? testPostCode : postCode)
                                       .Replace("{apikey}", apiKey);

                using (var client = new WebClient())
                {
                    var result = await client.DownloadStringTaskAsync(new Uri(requestUrl));

                    if (!isTestMode)
                        await Database.Save(new PostCodeHistory
                        {
                            PostCode = postCode,
                            Result = result,
                            AddedOn = LocalTime.Now
                        });

                    return result;
                }
            }
        }
    }
}
