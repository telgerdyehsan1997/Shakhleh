using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Olive;

namespace Controllers
{
    public partial class AdminSettingsFileErroLogController
    {

        private byte[] GetDownloadFile(ReportErrorLog log)
        {
            var unprocessedFolderPath = Config.Get<string>("FileProcessor:UnprocessedFolderPath");
            var fileName = Path.Combine(new[] { unprocessedFolderPath, log.Location, log.FileName });
            return System.IO.File.ReadAllBytes(fileName);
        }

    }
}
