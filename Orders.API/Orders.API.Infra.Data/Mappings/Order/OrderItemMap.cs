using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Orders.API.Domain.Entities.Order;

namespace Orders.API.InfraData.Mappings.Order
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.HasKey(x => x.ItemCode);
            //.HasKey(c => new { c.Id, c.AccountId });

            builder
                .Property(c => c.ItemCode)
                .HasColumnName("item_code") // sql join easily
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GuidValueGenerator>()
                .IsRequired();

            builder
                .Property(c => c.ItemDescription)
                .HasColumnName("item_description")
                .HasColumnType("varchar(200)");

            builder
                .Property(c => c.ItemSku)
                .HasColumnName("item_sku")
                .HasColumnType("varchar(200");

        }
    }
}
