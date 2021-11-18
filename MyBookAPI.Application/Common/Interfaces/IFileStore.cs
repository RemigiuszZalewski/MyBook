namespace MyBookAPI.Application.Common.Interfaces
{
    public interface IFileStore
    {
        string WriteFile(byte[] content, string sourceFileName, string path);
    }
}
