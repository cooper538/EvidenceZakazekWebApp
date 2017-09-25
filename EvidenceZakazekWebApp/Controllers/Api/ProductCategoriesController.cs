using EvidenceZakazekWebApp.Persistence;
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

        [HttpDelete]
        public IHttpActionResult Detele(int id)
        {
            var productCategory = _unitOfWork.ProductCategories // TODO: Zkontrolovat dodržení rozdílu mezi remove a Delete
                .GetCategoryWithProductsAndProperties(id);

            if (productCategory == null)
                return NotFound();

            _unitOfWork.ProductCategories.RemoveWithProductsWithProperties(productCategory);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
