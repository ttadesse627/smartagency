

namespace AppDiv.SmartAgency.Utility.Contracts;
public class FileResult
{
    public byte[] File { get; set; } = new byte[0];
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
}