using EvidenceZakazekWebApp.Persistence;
using System.Web.Http;
using EvidenceZakazekWebApp.Core;

namespace EvidenceZakazekWebApp.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Detele(int id)
        {
            var product = _unitOfWork.Products.GetProductWithProperties(id);

            if (product == null)
                return NotFound();

            _unitOfWork.Products.RemoveWithValues(product);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
