using AutoMapper;
using EvidenceZakazekWebApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace EvidenceZakazekWebApp.Controllers.Api
{
    public class ProductCategoriesController : ApiController
    {

        ApplicationDbContext _context;
        IMapper _mapper;

        public ProductCategoriesController()
        {
            _context = new ApplicationDbContext();
            _mapper = MvcApplication.MapperConfiguration.CreateMapper();
        }

        [HttpDelete]
        public IHttpActionResult Detele(int id)
        {
            var categoryForDelete = _context.ProductCategories
                .Include(pc => pc.PropertyDefinitions.Select(pd => pd.PropertyValues))
                .Include(pc => pc.Products)
                .Single(p => p.Id == id);

            if (categoryForDelete == null)
                return NotFound();

            foreach (var propertyDefinition in categoryForDelete.PropertyDefinitions)
            {
                _context.PropertyValues.RemoveRange(propertyDefinition.PropertyValues);
            }

            _context.PropertyDefinitions.RemoveRange(categoryForDelete.PropertyDefinitions);
            _context.Products.RemoveRange(categoryForDelete.Products);

            _context.ProductCategories.Remove(categoryForDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}
