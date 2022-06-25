using Microsoft.AspNetCore.Http;
using Olive;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Domain
{

    public class UploadContract
    {
        public int TotalFilesReceived { get; set; }
        public long TotalFilesSize { get; set; }

        public string Message { get; set; }

        public List<FileSaveStatusContract> FileSaveStatuses { get; set; } = new List<FileSaveStatusContract>();
    }

    public class FileSaveStatusContract
    {
        public string FileName { get; set; }
        public bool IsExtentionAccepted { get; set; }
        public bool IsSaved { get; set; }
        public string Message { get; set; }
    }


    public class HMRCResponse
    {
        public string error { get; set; }
        public bool success { get; set; }
        public string status { get; set; }
    }
}
