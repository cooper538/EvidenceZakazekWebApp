using System.Collections.Generic;
using System.Linq;
using EvidenceZakazekWebApp.Core.Models;
using EvidenceZakazekWebApp.Core.Repositories;

namespace EvidenceZakazekWebApp.Persistence.Repositories
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