using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface ITaxRepository : IRepositoryBase<Tax, Guid>
    {
        Task<(IList<Tax> data, int total, int totalDisplay)> GetAllTaxesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<IList<Tax>> GetOrderedTaxes();
    }
}
