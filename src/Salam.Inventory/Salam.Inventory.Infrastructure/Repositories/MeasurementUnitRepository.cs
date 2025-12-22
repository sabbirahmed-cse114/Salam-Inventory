using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;
//using Salam.Inventory.Infrastructure.UnitOfWorks;
using Salam.Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class MeasurementUnitRepository : Repository<MeasurementUnit, Guid>, IMeasurementUnitRepository
    {
        public MeasurementUnitRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public bool IsMeasurementUnitDuplicateOrNot(string title, Guid? id = null)
        {
            if (id.HasValue)
            {
                return GetCount(x => !x.Id.Equals(id.Value) && x.UnitName.Equals(title)) > 0;
            }
            else
            {
                return GetCount(x => x.UnitName.Equals(title)) > 0;
            }
        }

        public async Task<(IList<MeasurementUnit> data, int total, int totalDisplay)> GetPagedMeasurementUnitsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            var searchText = search.Value;
            if (string.IsNullOrWhiteSpace(searchText))
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);

            return await GetDynamicAsync(x => x.UnitName.Contains(searchText) ||
                                         x.Symbol.Contains(searchText),
                                         order, null, pageIndex, pageSize, true);
        }
    }
}