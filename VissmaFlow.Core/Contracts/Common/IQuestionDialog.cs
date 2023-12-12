namespace VissmaFlow.Core.Contracts.Common
{
    public interface IQuestionDialog
    {
        Task<bool> Ask(string title, string message);
    }
}
