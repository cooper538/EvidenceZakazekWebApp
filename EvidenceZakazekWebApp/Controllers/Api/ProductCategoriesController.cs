using EvidenceZakazekWebApp.Core;
using System.Web.Http;

namespace EvidenceZakazekWebApp.Controllers.Api
{
    public class ProductCategoriesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // TODO: Vyřešit Flush message u obou api controlerů přes JS, něco jako If NotFounf then Flush message. Vytvořit servisu?
        [HttpDelete]
        public IHttpActionResult Detele(int id)
        {
            //Todo: try-catch

            var productCategory = _unitOfWork.ProductCategories
                .GetCategoryWithProductsAndProperties(id);

            if (productCategory == null)
                return NotFound();


            _unitOfWork.ProductCategories.RemoveWithProductsWithProperties(productCategory);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
