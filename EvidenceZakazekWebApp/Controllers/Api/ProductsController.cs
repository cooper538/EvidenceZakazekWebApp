using AutoMapper;
using EvidenceZakazekWebApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace EvidenceZakazekWebApp.Controllers.Api
{
    public class ProductsController : ApiController
    {
        ApplicationDbContext _context;
        IMapper _mapper;

        public ProductsController()
        {
            _context = new ApplicationDbContext();
            _mapper = MvcApplication.MapperConfiguration.CreateMapper();
        }

        [HttpDelete]
        public IHttpActionResult Detele(int id)
        {
            var productForDelete = _context.Products
                .Include(p => p.PropertyValues)
                .Single(p => p.Id == id);

            if (productForDelete == null)
                return NotFound();

            _context.PropertyValues.RemoveRange(productForDelete.PropertyValues);

            _context.Products.Remove(productForDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}
