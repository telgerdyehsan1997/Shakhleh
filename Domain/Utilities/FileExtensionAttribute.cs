using System;

namespace Domain
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class FileExtensionAttribute : Attribute
    {
        private readonly string _fileType;
        public string FileType => _fileType;

        public FileExtensionAttribute() : base() { }

        public FileExtensionAttribute(string fileType) : base()
        {
            _fileType = fileType;
        }
    }
}
