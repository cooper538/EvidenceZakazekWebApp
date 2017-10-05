using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Helpers.FlashMessagesHelper
{
    public static class FlashHtmlHelper
    {
        // Rozšířitelné o funkci Closable
        public static MvcHtmlString RenderFlashMessages(this HtmlHelper htmlHelper)
        {
            var flashMessages = GetFlashMessages(htmlHelper);

            var outerDiv = new TagBuilder("div");
            outerDiv.AddCssClass("allertsContainer");

            var flashMessagesHtml = new StringBuilder();

            foreach (var flashMessage in flashMessages)
            {
                var alertDiv = new TagBuilder("div");
                alertDiv.AddCssClass($"alert {flashMessage.GetClass()}");
                alertDiv.SetInnerText(flashMessage.Message);

                flashMessagesHtml.Append(alertDiv);
            }
            outerDiv.InnerHtml = flashMessagesHtml.ToString();

            return MvcHtmlString.Create(outerDiv.ToString());
        }

        private static IEnumerable<FlashMessage> GetFlashMessages(HtmlHelper htmlHelper)
        {
            var flashMessages = htmlHelper.ViewContext.TempData[FlashControllerExtensions.FlashTempDataKey] as IEnumerable<FlashMessage>
                   ?? new List<FlashMessage>();

            ClearTempData(htmlHelper);
            return flashMessages;
        }

        private static void ClearTempData(HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.TempData.Remove(FlashControllerExtensions.FlashTempDataKey);
        }
    }
}