using EvidenceZakazekWebApp.Core;
using EvidenceZakazekWebApp.Core.Models;
using EvidenceZakazekWebApp.Core.Repositories;
using EvidenceZakazekWebApp.Persistence.Repositories;

namespace EvidenceZakazekWebApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository Products { get; private set; }
        public IProductCategoryRepository ProductCategories { get; private set; }
        public ISupplierRepository Suppliers { get; private set; }
        public IPropertyValueRepository PropertyValues { get; private set; }
        public IPropertyDefinitionRepository PropertyDefinitions { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            ProductCategories = new ProductCategoryRepository(_context);
            Suppliers = new SupplierRepository(_context);
            PropertyValues = new PropertyValueRepository(_context);
            PropertyDefinitions = new PropertyDefinitionRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}