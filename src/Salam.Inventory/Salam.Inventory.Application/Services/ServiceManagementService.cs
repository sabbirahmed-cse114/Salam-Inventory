using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public class ServiceManagementService : IServiceManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public ServiceManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task CreateService_Async(Service service)
        {
            var isServiceDupliacate = _inventoryUnitOfWork.ServiceRepository.IsServiceNameDuplicateOrNot(service.Name);

            if (!isServiceDupliacate)
            {
                await _inventoryUnitOfWork.ServiceRepository.AddAsync(service);
                await _inventoryUnitOfWork.SaveAsync();
            }
        }

        public async Task<(IList<Service> data, int total, int totalDisplay)> GetServices_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _inventoryUnitOfWork.ServiceRepository.GetServicesAsList(pageIndex, pageSize, search, order);
        }

        public async Task<Service> GetService_Async(Guid serviceId)
        {
            return await _inventoryUnitOfWork.ServiceRepository.GetService_Async(serviceId);
        }

        public async Task DeleteService_Async(Guid id)
        {
            await _inventoryUnitOfWork.ServiceRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task UpdateService_Async(Service service)
        {
            var isServiceDuplicate = _inventoryUnitOfWork.ServiceRepository.IsServiceNameDuplicateOrNot(service.Name, service.Id);
            if (!isServiceDuplicate)
            {
                await _inventoryUnitOfWork.ServiceRepository.EditAsync(service);
                await _inventoryUnitOfWork.SaveAsync();
            }
        }

        public async Task<double> GetServicesCount_Async()
        {
            return await _inventoryUnitOfWork.ServiceRepository.GetCountAsync();
        }
    }
}