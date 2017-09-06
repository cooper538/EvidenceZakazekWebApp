using AutoMapper;
using EvidenceZakazekWebApp.Dtos;
using EvidenceZakazekWebApp.Dtos.Interfaces;
using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
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

        public ActionResult Index(int id = 1)
        {
            var products = _context.ProductCategories
                .Include(pc => pc.Products.Select(p => p.ProductCategory))
                .Include(pc => pc.Products.Select(p => p.Supplier))
                .Include(pc => pc.Products.Select(p => p.PropertyValues.Select(pv => pv.PropertyDefinition)))
                .Single(pc =>pc.Id == id)
                .Products
                .ToList();

            var productDtos = _mapper.Map<List<Product>, List<ProductDto>>(products);
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
                Heading = "Přidej produkt",
                Suppliers = _context.Suppliers.ToList(),
                Categories = _context.ProductCategories.ToList()
            };

            return View("ProductForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Suppliers = _context.Suppliers.ToList();
                viewModel.Categories = _context.ProductCategories.ToList();
                return View("ProductForm", viewModel);
            }

            var product = new Product()
            {
                Name = viewModel.Name,
                OrderNumber = viewModel.OrderNumber,
                TypeName = viewModel.TypeName,
                Price = viewModel.Price,
                SupplierId = viewModel.SupplierId,
                ProductCategoryId = viewModel.CategoryId,
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

            return RedirectToAction("Index", "Products");
        }

        public ActionResult Edit(int id)
        {
            var product = _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.ProductCategory)
                .Include(p => p.PropertyValues.Select(pv => pv.PropertyDefinition))
                .Single(p => p.Id == id);

            var viewModel = new ProductFormViewModel
            {
                Heading = $"Editace produktu s id:{product.Id}",
                Id = product.Id,
                Name = product.Name,
                OrderNumber = product.OrderNumber,
                TypeName = product.TypeName,
                Price = product.Price,
                SupplierId = product.SupplierId, // rename supplier to supplierId, same for Category
                Suppliers = _context.Suppliers.ToList(),
                CategoryId = product.ProductCategoryId,
                Categories = _context.ProductCategories.ToList(),
                PropertyValues = product.PropertyValues.Select(pv =>
                    new PropertyValueFormViewModel
                    {
                        PropertyDefinitionId = pv.PropertyDefinitionId,
                        PropertyDefinitionName = pv.PropertyDefinition.Name,
                        Value = pv.Value,
                        MeasureUnit = pv.PropertyDefinition.MeasureUnit
                    }
                ).ToList()
            };

            return View("ProductForm", viewModel);
        }

        public ActionResult Update(ProductFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Suppliers = _context.Suppliers.ToList();
                viewModel.Categories = _context.ProductCategories.ToList();
                return View("ProductForm", viewModel);
            }

            var product = _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.ProductCategory)
                .Include(p => p.PropertyValues.Select(pv => pv.PropertyDefinition))
                .Single(p => p.Id == viewModel.Id);

            var oldPropertyValues = _context.PropertyValues
                .Where(pv => pv.ProductId == product.Id)
                .ToList();

            _context.PropertyValues.RemoveRange(oldPropertyValues);

            product.Name = viewModel.Name;
            product.OrderNumber = viewModel.OrderNumber;
            product.TypeName = viewModel.TypeName;
            product.Price = viewModel.Price;
            product.SupplierId = viewModel.SupplierId;
            product.ProductCategoryId = viewModel.CategoryId;

            product.PropertyValues = viewModel.PropertyValues.Select(pv =>
                new PropertyValue
                {
                    Value = pv.Value,
                    PropertyDefinitionId = pv.PropertyDefinitionId
                }
            ).ToList();

            _context.SaveChanges();

            return RedirectToAction("Index");
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