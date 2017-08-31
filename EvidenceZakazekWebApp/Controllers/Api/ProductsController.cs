using AutoMapper;
using EvidenceZakazekWebApp.Models;
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
    }
}
