using AutoMapper;
using EvidenceZakazekWebApp.Dtos;
using EvidenceZakazekWebApp.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public IEnumerable<ProductDto> GetProducts()
        {
            var products = _context.Products
                .Include(p => p.Supplier)
                .ToList();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
