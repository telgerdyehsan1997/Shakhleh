using Olive;
using Olive.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Domain
{

    public interface IZipDownloadService
    {
        Task<Blob> CompressFiles(IEnumerable<Blob> files, string filename);
    }

    public class ZipDownloadService : IZipDownloadService
    {
        private readonly IDatabase Database = Context.Current.Database();

        public Task<Blob> CompressFiles(IEnumerable<Blob> files, string filename) => ZipFiles(files, filename);

        private async Task<Blob> ZipFiles(IEnumerable<Blob> files, string filename)
        {

            using (var compressedFileStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                {
                    foreach (var file in files)
                    {
                        var zipEntry = zipArchive.CreateEntry(file.FileName);
                        using (var originalFileStream = new MemoryStream(await file.GetFileDataAsync()))
                        using (var zipEntryStream = zipEntry.Open())
                            originalFileStream.CopyTo(zipEntryStream);
                    }

                }
                return new Blob(compressedFileStream.ToArray(), filename + ".zip");
            }
        }
    }
}