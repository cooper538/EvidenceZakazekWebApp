﻿using AutoMapper;
using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
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
                .Single(pc => pc.Id == id)
                .Products
                .ToList();

            var crudRowViewModels = _mapper.Map<List<Product>, List<CrudRowViewModel>>(products);

            var viewModel = new CrudTableViewModel()
            {
                Heading = "Produkty",
                ControllerName = "products",
                CrudRowViewModels = crudRowViewModels
            };

            return View("CrudTable", viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new ProductFormViewModel
            {
                Heading = "Přidej produkt",
                Suppliers = _context.Suppliers.ToList(),
                ProductCategories = _context.ProductCategories.ToList()
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
                viewModel.ProductCategories = _context.ProductCategories.ToList();
                return View("ProductForm", viewModel);
            }

            var product = _mapper.Map<ProductFormViewModel, Product>(viewModel);

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
                Suppliers = _context.Suppliers.ToList(),
                ProductCategories = _context.ProductCategories.ToList()
            };

            _mapper.Map<Product, ProductFormViewModel>(product, viewModel);

            return View("ProductForm", viewModel);
        }

        public ActionResult Update(ProductFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Suppliers = _context.Suppliers.ToList();
                viewModel.ProductCategories = _context.ProductCategories.ToList();
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
            product.ProductCategoryId = viewModel.ProductCategoryId;

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
            var product = _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.PropertyValues.Select(pv => pv.PropertyDefinition))
                .SingleOrDefault(p => p.Id == id);

            var viewModel = new DetailViewModel()
            {
                Heading = "Detail produktu s id:" + id,
                ControllerName = "Products" // TODO: refaktoring to dynamic name, no magic strings
            };

            _mapper.Map(product, viewModel);

            return View("Detail", viewModel);
        }
    }
}