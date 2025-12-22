using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;


namespace Salam.Inventory.Application.Services
{
    public interface IMeasurementUnitManagementService
    {
        Task CreateMeasurementUnit_Async(MeasurementUnit measurementUnit);
        Task<(IList<MeasurementUnit> data, int total, int totalDisplay)> GetMeasurementUnits_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<MeasurementUnit> GetMeasurementUnit_Async(Guid id);
        Task UpdateMeasurementUnit_Async(MeasurementUnit measurementUnit);
        Task DeleteMeasurementUnit_Async(Guid id);
        Task<IList<MeasurementUnit>> GetMeasurementUnits_Async();
    }
}
