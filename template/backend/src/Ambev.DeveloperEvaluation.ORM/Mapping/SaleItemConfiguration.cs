using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.HasKey(si => si.Id);

            builder.Property(si => si.Quantity)
                .IsRequired();

            builder.Property(si => si.Discount)
                .HasColumnType("decimal(18,2)");

            // Relacionamento com o Produto
            builder.HasOne(si => si.Product)
                .WithMany(p => p.SaleItems) // Caso o Product não possua uma coleção de SaleItems
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
