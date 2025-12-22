namespace Salam.Inventory.Application.Services
{
    public class DisplayLogService : IDisplayLogService
    {
        private IInventoryUnitOfWork _inventoryUnitOfWork;
        public DisplayLogService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

    }
}
