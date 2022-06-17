using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.API.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Orders.API.InfraData.Mappings.Order
{
    public class EcomOrderMap : IEntityTypeConfiguration<EcomOrder>
    {
        public void Configure(EntityTypeBuilder<EcomOrder> builder)
        {

            builder.HasKey(x => x.OrderNumber);
            //.HasKey(c => new { c.Id, c.AccountId });

            builder
                .Property(c => c.OrderNumber)
                .HasColumnName("order_number") // sql join easily
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GuidValueGenerator>()
                .IsRequired();

            builder
                .Property(c => c.Status)
                .HasColumnName("status")
                .HasColumnType("uuid")
                .IsRequired();

            builder
                .Property(c => c.OrderIdentifier)
                .HasColumnName("active")
                .HasColumnType("boolean")
                .HasDefaultValue(false)
                .IsRequired();

            builder
                .Property(c => c.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime");

        }
    }
}
