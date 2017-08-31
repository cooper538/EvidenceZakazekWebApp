using AutoMapper;
using EvidenceZakazekWebApp.Models;
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
    }
}
