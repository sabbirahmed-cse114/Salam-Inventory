using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public class TaxManagementService : ITaxManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public TaxManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task<(IList<Tax> data, int total, int totalDisplay)> GetTaxes_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _inventoryUnitOfWork.TaxRepository.GetAllTaxesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<Tax> GetTax_Async(Guid taxId)
        {
            return await _inventoryUnitOfWork.TaxRepository.GetByIdAsync(taxId);
        }

        public async Task<IList<Tax>> GetTaxList_Async()
        {
            return await _inventoryUnitOfWork.TaxRepository.GetOrderedTaxes();
        }
    }
}