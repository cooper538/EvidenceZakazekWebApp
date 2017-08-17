using AutoMapper;
using EvidenceZakazekWebApp.Dtos;
using EvidenceZakazekWebApp.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Reflection;
using System.Collections;
using Newtonsoft.Json;

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

        public IEnumerable<ProductDto> GetProducts()
        {
            var products = _context.Products
                .Include(p => p.Supplier)
                .ToList();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        [Route("~/api/products/forTable")]
        public IHttpActionResult GetProductsForTable()
        {
            var products = _context.Products
                .Include(p => p.Supplier)
                .ToList();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            // from https://stackoverflow.com/a/10542548/6355668
            // without serialize it (problem with escaping)
            var tableData = GetObjectArray(productDtos);

            var columnHeaders = new[] {
                new { title = "Id" },
                new { title = "Jméno" },
                new { title = "Objednací číslo" },
                new { title = "Typové označení" },
                new { title = "Cena" },
                new { title = "Dodavatel" }
            };
            
            return Ok(new {
                data = tableData,
                columns = columnHeaders
            }); 
        }

        public static IEnumerable<object> GetObjectArray<T>(IEnumerable<T> obj)
        {
            return obj.Select(o => o.GetType().GetProperties().Select(p => p.GetValue(o, null)));
        }
    }
}
