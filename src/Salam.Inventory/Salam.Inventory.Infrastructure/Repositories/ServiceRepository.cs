using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;
using Salam.Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class ServiceRepository : Repository<Service, Guid>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IList<Service> data, int total, int totalDisplay)> GetServicesAsList(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await GetDynamicAsync(null, order, x => x.Include(y => y.Tax), pageIndex, pageSize, true);
        }

        public async Task<Service> GetService_Async(Guid serviceId)
        {
            return await GetByIdAsync(x => x.Id.Equals(serviceId), y => y.Include(z => z.Tax));
        }

        public bool IsServiceNameDuplicateOrNot(string title, Guid? id = null)
        {
            if (id.HasValue)
            {
                return GetCount(x => !x.Id.Equals(id.Value) && x.Name.Equals(title)) > 0;
            }
            else
            {
                return GetCount(x => x.Name.Equals(title)) > 0;
            }
        }
    }
}