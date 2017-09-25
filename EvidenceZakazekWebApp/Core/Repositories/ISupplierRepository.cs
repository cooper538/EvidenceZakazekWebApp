using System.Collections.Generic;
using EvidenceZakazekWebApp.Core.Models;

namespace EvidenceZakazekWebApp.Core.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
    }
}