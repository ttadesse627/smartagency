

namespace AppDiv.SmartAgency.Utility.Contracts;
public class FileModel
{
    public string Base64String { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string PathToSave { get; set; } = string.Empty;
    public FileMode FileMode { get; set; }
}