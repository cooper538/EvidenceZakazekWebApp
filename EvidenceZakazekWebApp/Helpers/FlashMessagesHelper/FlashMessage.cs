namespace EvidenceZakazekWebApp.Helpers.FlashMessagesHelper
{
    public class FlashMessage
    {
        public FlashMessage(FlashMessageType type, string message, int delayInMiliseconds)
        {
            Type = type;
            Message = message;
            DelayToHideMiliseconds = delayInMiliseconds;
        }

        public FlashMessageType Type { get; }
        public string Message { get; }
        public int DelayToHideMiliseconds { get; }

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