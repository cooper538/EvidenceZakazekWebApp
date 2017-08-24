using AutoMapper;
using EvidenceZakazekWebApp.Dtos;
using EvidenceZakazekWebApp.Helpers;
using EvidenceZakazekWebApp.Models;
using System.Collections.Generic;
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

        [Route("~/api/products/forTable")]
        public IHttpActionResult GetProductsForTable()
        {
            var products = _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.ProductCategory)
                .ToList();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            // from https://stackoverflow.com/a/10542548/6355668
            // without serialize it (problem with escaping)
            var tableData = ObjectHelper.GetObjectArray(productDtos);

            var columnHeaders = new[] {
                new { title = "Id" },
                new { title = "Jméno" },
                new { title = "Kategorie" },
                new { title = "Objednací číslo" },
                new { title = "Typové označení" },
                new { title = "Cena" },
                new { title = "Dodavatel" }
            };

            return Ok(new
            {
                data = tableData,
                columns = columnHeaders
            });
        }
    }
}
