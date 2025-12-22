using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public interface ITaxManagementService
    {
        Task<(IList<Tax> data, int total, int totalDisplay)> GetTaxes_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order);

        Task<Tax> GetTax_Async(Guid taxId);
        Task<IList<Tax>> GetTaxList_Async();
    }
}