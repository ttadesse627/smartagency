using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Utility.Contracts
{
    public class FileResponse
    {
        public string? File { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;

    }
}