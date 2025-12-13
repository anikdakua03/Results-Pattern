namespace AllResultsPattern.MyCustomResults;

public class ResultMessage
{
    public string Text { get; }

    public MessageType Type { get; }

    public string Severity => Type.ToString();

    private ResultMessage(string text, MessageType type)
    {
        Text = text ?? throw new ArgumentNullException("Text message cannot be null");
        Type = type;
    }

    #region Implicit conversions

    public static implicit operator string(ResultMessage message) => message.Text;

    public static implicit operator ResultMessage(string text) => new (text, MessageType.Information);

    #endregion

    #region Factory methods

    public static ResultMessage Success(string message = "Request successful") => new (message, MessageType.Success);

    public static ResultMessage Information(string information) => new (information, MessageType.Information);

    public static ResultMessage Warning(string warning) => new (warning, MessageType.Warning);

    public static ResultMessage Failure(string message = "Request failed.") => new (message, MessageType.Failure);

    #endregion

    public override string ToString() => Text;
}
