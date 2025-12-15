using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface IServiceRepository : IRepositoryBase<Service, Guid>
    {
        bool IsTitleDuplicateOrNot(string title, Guid? id = null);
        Task<(IList<Service> data, int total, int totalDisplay)> GetServiceList(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<Service> GetServiceAsync(Guid serviceId);
    }
}