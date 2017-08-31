using System.Collections.Generic;

namespace EvidenceZakazekWebApp.Dtos.Interfaces
{
    public interface ICrudTableDto
    {
        int Id { get; set; }
        ICollection<string> ColumnNames { get; }
        IDictionary<string, string> Properties { get; }
    }
}