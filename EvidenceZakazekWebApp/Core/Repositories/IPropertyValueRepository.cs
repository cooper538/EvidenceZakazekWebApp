namespace EvidenceZakazekWebApp.Core.Repositories
{
    public interface IPropertyValueRepository
    {
        void RemoveValuesByProduct(int productId);
    }
}