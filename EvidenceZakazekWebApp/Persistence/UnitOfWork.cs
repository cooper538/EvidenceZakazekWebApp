using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.Repositories;

namespace EvidenceZakazekWebApp.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository Products { get; private set; }
        public ProductCategoryRepository ProductCategories { get; private set; }
        public SupplierRepository Suppliers { get; private set; }
        public PropertyValueRepository PropertyValues { get; set; }
        public PropertyDefinitionRepository PropertyDefinitions { get; set; }

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