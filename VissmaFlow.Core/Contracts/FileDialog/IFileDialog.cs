namespace VissmaFlow.Core.Contracts.FileDialog
{
    public interface IFileDialog
    {
        Task<string> GetPath();
    }
}
