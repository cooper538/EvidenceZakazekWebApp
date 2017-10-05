using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Helpers.FlashMessagesHelper
{
    public static class FlashControllerExtensions
    {
        internal const string FlashTempDataKey = "FlashMessages";

        public static void AddFlashMessage(this Controller controller, FlashMessageType key, string message)
        {
            ICollection<FlashMessage> flashMessages = new List<FlashMessage>();
            var oldFlashMesseges = controller.TempData[FlashTempDataKey] as IEnumerable<FlashMessage>;

            if (oldFlashMesseges != null)
                flashMessages = flashMessages.Concat(oldFlashMesseges).ToList();

            var flashMessage = new FlashMessage(key, message);
            flashMessages.Add(flashMessage);

            controller.TempData["FlashMessages"] = flashMessages;
        }
    }
}