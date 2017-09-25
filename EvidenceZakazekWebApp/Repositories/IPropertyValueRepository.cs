namespace EvidenceZakazekWebApp.Repositories
{
    public interface IPropertyValueRepository
    {
        void RemoveValuesByProduct(int productId);
    }
}