namespace VissmaFlow.View.Dialogs.Common;

public partial class QuestionControl : DialogWindow
{
    public QuestionControl(string question)
    {
        Question = question;
        InitializeComponent();
    }

    public string  Question { get; set; }
}