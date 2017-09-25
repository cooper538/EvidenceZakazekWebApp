using EvidenceZakazekWebApp.Models;
using System.Collections.Generic;

namespace EvidenceZakazekWebApp.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
    }
}