using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public interface IServiceManagementService
    {
        Task CreateService_Async(Service service);
        Task<(IList<Service> data, int total, int totalDisplay)> GetServices_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task DeleteService_Async(Guid id);
        Task<Service> GetService_Async(Guid id);
        Task UpdateService_Async(Service service);
        Task<double> GetServicesCount_Async();
    }
}