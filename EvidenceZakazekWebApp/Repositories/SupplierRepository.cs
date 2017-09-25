using EvidenceZakazekWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace EvidenceZakazekWebApp.Repositories
{

    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _context.Suppliers.ToList();
        }
    }
}