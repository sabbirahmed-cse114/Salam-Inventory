using Autofac;
using Salam.Inventory.Application;
using Salam.Inventory.Application.Services;
using Salam.Inventory.Domain.RepositoryContracts;
using Salam.Inventory.Infrastructure;
using Salam.Inventory.Infrastructure.Repositories;
using Salam.Inventory.Infrastructure.UnitOfWorks;
using Salam.Inventory.Infrastructure.Utilities;

namespace Salam.Inventory.Web
{
    public class WebModule(string connectionString, string migrationAssembly) : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", connectionString)
                .WithParameter("migrationAssembly", migrationAssembly)
                .InstancePerLifetimeScope();

            builder.RegisterType<InventoryUnitOfWork>()
                .As<IInventoryUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryManagementService>()
                .As<ICategoryManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>()
                .As<ICategoryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MeasurementUnitManagementService>()
                .As<IMeasurementUnitManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MeasurementUnitRepository>()
                .As<IMeasurementUnitRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TaxManagementService>()
                .As<ITaxManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TaxReporsitory>()
                .As<ITaxRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ServiceManagementService>()
               .As<IServiceManagementService>()
               .InstancePerLifetimeScope();

            builder.RegisterType<ServiceRepository>()
                .As<IServiceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductManagementService>()
               .As<IProductManagementService>()
               .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>()
              .As<IProductRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<ImageServiceUtility>()
              .As<IImageServiceUtility>()
              .InstancePerLifetimeScope();

            builder.RegisterType<WarehouseManagementService>()
               .As<IWarehouseManagementService>()
               .InstancePerLifetimeScope();

            builder.RegisterType<WarehouseRepository>()
              .As<IWarehouseRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<ProductWarehouseRepository>()
             .As<IProductWarehouseRepository>()
             .InstancePerLifetimeScope();

            builder.RegisterType<StockTransferRepository>()
              .As<IStockTransferRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<StockTransferManagementService>()
             .As<IStockTransferManagementService>()
             .InstancePerLifetimeScope();

            builder.RegisterType<StockTransferProductRepository>()
              .As<IStockTransferProductRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<StockAdjustmentReasonRepository>()
              .As<IStockAdjustmentReasonRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<StockAdjustmentProductRepository>()
              .As<IStockAdjustmentProductRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<StockAdjustmentRepository>()
              .As<IStockAdjustmentRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<StockAdjustmentManagementService>()
             .As<IStockAdjustmentManagementService>()
             .InstancePerLifetimeScope();

            builder.RegisterType<DisplayStockListService>()
             .As<IDisplayStockListService>()
             .InstancePerLifetimeScope();

            builder.RegisterType<RedirectIfLoggedin>().AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}