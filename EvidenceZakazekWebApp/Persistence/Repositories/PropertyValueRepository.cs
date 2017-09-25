using System.Linq;
using EvidenceZakazekWebApp.Core.Repositories;

namespace EvidenceZakazekWebApp.Persistence.Repositories
{
    public class PropertyValueRepository : IPropertyValueRepository
    {
        private readonly ApplicationDbContext _context;

        public PropertyValueRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void RemoveValuesByProduct(int productId)
        {
            var valuesForRemove = _context.PropertyValues
                .Where(pv => pv.ProductId == productId)
                .ToList();

            _context.PropertyValues.RemoveRange(valuesForRemove);
        }
    }
}