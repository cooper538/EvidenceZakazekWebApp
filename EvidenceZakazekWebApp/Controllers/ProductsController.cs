﻿using AutoMapper;
using EvidenceZakazekWebApp.Dtos;
using EvidenceZakazekWebApp.Dtos.Interfaces;
using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.Controllers
{
    public class ProductsController : Controller
    {
        ApplicationDbContext _context;
        IMapper _mapper;

        public ProductsController()
        {
            _context = new ApplicationDbContext();
            _mapper = MvcApplication.MapperConfiguration.CreateMapper();

        }

        public ActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Supplier)
                .Include(p => p.PropertyValues.Select(pv => pv.PropertyDefinition))
                .Where(p => p.ProductCategory.Name == "Test")
                .ToList();

            var productDtos = _mapper.Map< List<Product>, List<ProductDto>>(products);
            var productTableDtos = _mapper.Map<List<ProductDto>, List<ProductTableDto>>(productDtos);

            var CrudtableDtos = new Collection<ICrudTableDto>();
            productTableDtos.ForEach(ptd => CrudtableDtos.Add(ptd));

            var viewModel = new CrudTableViewModel()
            {
                Heading = "Produkty",
                ControllerName = "products",
                CrudTableDtos = CrudtableDtos
            };

            return View("CrudTable", viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new ProductFormViewModel
            {
                Suppliers = _context.Suppliers.ToList(),
                Categories = _context.ProductCategories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Suppliers = _context.Suppliers.ToList();
                viewModel.Categories = _context.ProductCategories.ToList();
                return View("Create", viewModel);
            }

            var product = new Product()
            {
                Name = viewModel.Name,
                OrderNumber = viewModel.OrderNumber,
                TypeName = viewModel.TypeName,
                Price = viewModel.Price,
                SupplierId = viewModel.Supplier,
                ProductCategoryId = viewModel.Category,
                PropertyValues = viewModel.PropertyValues.Select(
                    pv => new PropertyValue()
                    {
                        Value = pv.Value,
                        PropertyDefinitionId = pv.PropertyDefinitionId
                    }
                ).ToList()
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Detail(int id)
        {
            var viewModel = new DetailViewModel()
            {
                Heading = "Detail produktu s id:" + id,
                ControllerName = "Products" // TODO: refaktoring to dynamic name, no magic strings
            };

            var product = _context.Products
                .Include(p => p.PropertyValues.Select(pv => pv.PropertyDefinition))
                .SingleOrDefault(p => p.Id == id);

            var staticProperties = new Dictionary<string, string>()
            {
                //{ "Id", product.Id.ToString() },
                { "Jméno", product.Name },
                { "Objednací číslo", product.OrderNumber },
                { "Typové označení", product.TypeName },
                { "Cena", $"{product.Price} Kč"  }
            };

            // https://stackoverflow.com/a/953961/6355668
            var dynamicProperties = product.PropertyValues
                .ToDictionary(pv => pv.PropertyDefinition.Name, pv => $"{pv.Value} {pv.PropertyDefinition.MeasureUnit}");
            
            // https://stackoverflow.com/a/10559415/2756329
            var properties = staticProperties.Union(dynamicProperties)
                .ToDictionary(p => p.Key, p => p.Value);

            viewModel.Properties = properties;

            return View("Detail", viewModel);
        }
    }
}