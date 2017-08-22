using EvidenceZakazekWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using AutoMapper;
using EvidenceZakazekWebApp.Dtos;
using EvidenceZakazekWebApp.Helpers;

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

        [Route("~/api/productCategories/forTable")]
        public IHttpActionResult GetProductsForTable()
        {
            var productCategories = _context.ProductCategories
                .Include(pc => pc.PropertyDefinitions)
                .ToList();

            var productDtos = _mapper.Map<IEnumerable<ProductCategoryDto>>(productCategories);

            var tableData = new List<List<string>>();

            foreach (var productCategory in productCategories)
            {
                var categoryAttrs = new List<string>();

                categoryAttrs.Add(productCategory.Id.ToString());
                categoryAttrs.Add(productCategory.Name);
                tableData.Add(categoryAttrs);
            }

            // from https://stackoverflow.com/a/10542548/6355668
            // without serialize it (problem with escaping)


            var columnHeaders = new[] {
                new { title = "Id" },
                new { title = "Jméno" }
            };

            return Ok(new
            {
                data = tableData,
                columns = columnHeaders
            });
        }

    }
}
