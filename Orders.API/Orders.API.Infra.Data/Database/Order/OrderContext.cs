using Microsoft.EntityFrameworkCore;
using Orders.API.Domain.Entities.Order;
using Orders.API.InfraData.Mappings.Order;
using System.Reflection;

namespace Orders.API.InfraData.Database.Order
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        public DbSet<EcomOrder> EcomOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("ecomorder_context");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcomOrderMap).GetTypeInfo().Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderItemMap).GetTypeInfo().Assembly);
        }
    }
}
