using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface IServiceRepository : IRepositoryBase<Service, Guid>
    {
        bool IsServiceNameDuplicateOrNot(string title, Guid? id = null);
        Task<(IList<Service> data, int total, int totalDisplay)> GetServicesAsList(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<Service> GetService_Async(Guid serviceId);
    }
}