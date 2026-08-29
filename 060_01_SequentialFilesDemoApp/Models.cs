namespace SequentialFiles;

public class FileInformation
{
    public string FileName { get; set; } = "";
    public string FullPath { get; set; } = "";
    public string DirectoryName { get; set; } = "";
    public string Extension { get; set; } = "";
    public long Length { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime LastWriteTime { get; set; }
}

public class ExcelListInfo
{
    public string SerialNo { get; set; } = "";
    public string FileName { get; set; } = "";
    public string FilePath { get; set; } = "";
    public string FolderPath { get; set; } = "";
    public string Extension { get; set; } = "";
    public string ObjectFileType { get; set; } = "";
    public string FileContent { get; set; } = "";
}
