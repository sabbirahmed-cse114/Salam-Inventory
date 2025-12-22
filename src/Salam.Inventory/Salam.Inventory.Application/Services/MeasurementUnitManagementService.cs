using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public class MeasurementUnitManagementService : IMeasurementUnitManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public MeasurementUnitManagementService(IInventoryUnitOfWork inventoryUnitOfWork) 
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task<(IList<MeasurementUnit> data, int total, int totalDisplay)> GetMeasurementUnits_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return 
            await _inventoryUnitOfWork.MeasurementUnitRepository.GetPagedMeasurementUnitsAsync(pageIndex, pageSize, search, order);
        }

        public async Task CreateMeasurementUnit_Async(MeasurementUnit measurementUnit)
        {
            var isDuplicateMeasurementUnit = _inventoryUnitOfWork.MeasurementUnitRepository.IsMeasurementUnitDuplicateOrNot(measurementUnit.UnitName);
            if (!isDuplicateMeasurementUnit)
            {
                await _inventoryUnitOfWork.MeasurementUnitRepository.AddAsync(measurementUnit);
                await _inventoryUnitOfWork.SaveAsync();
            }
        }

        public async Task<MeasurementUnit> GetMeasurementUnit_Async(Guid id)
        {
            return await _inventoryUnitOfWork.MeasurementUnitRepository.GetByIdAsync(id);
        }

        public async Task UpdateMeasurementUnit_Async(MeasurementUnit measurementUnit)
        {
            var isDuplicate = _inventoryUnitOfWork.MeasurementUnitRepository.IsMeasurementUnitDuplicateOrNot(measurementUnit.UnitName, measurementUnit.Id);
            if (!isDuplicate)
            { 
                await _inventoryUnitOfWork.MeasurementUnitRepository.EditAsync(measurementUnit);
                await _inventoryUnitOfWork.SaveAsync();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async Task DeleteMeasurementUnit_Async(Guid id)
        {
            await _inventoryUnitOfWork.MeasurementUnitRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task<IList<MeasurementUnit>> GetMeasurementUnits_Async()
        {
            return await _inventoryUnitOfWork.MeasurementUnitRepository.GetAllAsync();
        }
    }
}