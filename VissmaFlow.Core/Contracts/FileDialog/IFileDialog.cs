namespace VissmaFlow.Core.Contracts.FileDialog
{
    public interface IFileDialog
    {
        Task<string> GetDirectory();

        Task<string> GetFile();
    }
}
