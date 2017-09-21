using AutoMapper;
using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.Persistence;
using System.Web.Http;

namespace EvidenceZakazekWebApp.Controllers.Api
{
    public class ProductCategoriesController : ApiController
    {

        private readonly UnitOfWork _unitOfWork;
        IMapper _mapper;

        public ProductCategoriesController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
            _mapper = MvcApplication.MapperConfiguration.CreateMapper();
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
