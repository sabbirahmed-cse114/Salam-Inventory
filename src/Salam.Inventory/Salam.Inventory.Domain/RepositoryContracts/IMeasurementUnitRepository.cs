using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface IMeasurementUnitRepository : IRepositoryBase<MeasurementUnit, Guid>
    {
        bool IsMeasurementUnitDuplicateOrNot(string title, Guid? id = null);
        Task<(IList<MeasurementUnit> data, int total, int totalDisplay)> GetPagedMeasurementUnitsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
