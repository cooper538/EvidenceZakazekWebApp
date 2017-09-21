using AutoMapper;
using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.Persistence;
using System.Web.Http;

namespace EvidenceZakazekWebApp.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;
        IMapper _mapper;

        public ProductsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
            _mapper = MvcApplication.MapperConfiguration.CreateMapper();
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
