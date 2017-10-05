namespace EvidenceZakazekWebApp.Helpers.FlashMessagesHelper
{
    public class FlashMessage
    {
        public FlashMessage(FlashMessageType type, string message)
        {
            Type = type;
            Message = message;
        }

        public FlashMessageType Type { get; }
        public string Message { get; }

        public string GetClass()
        {
            return "alert-" + Type.ToString().ToLower();
        }
    }

    public enum FlashMessageType
    {
        Info,
        Success,
        Warning,
        Danger
    }
}