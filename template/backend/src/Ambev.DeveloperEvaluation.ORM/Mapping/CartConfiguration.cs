using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartConfiguration
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            // Define o nome da tabela
            builder.ToTable("Carts");

            // Chave primária
            builder.HasKey(c => c.Id);

            // Propriedades
            builder.Property(c => c.UserId)
                   .IsRequired();

            builder.Property(c => c.Date)
                   .IsRequired()
                   .HasColumnType("datetime");

            // Relacionamento: um Cart tem muitos CartProducts
            builder.HasMany(c => c.Products)
                   .WithOne(cp => cp.Cart)
                   .HasForeignKey(cp => cp.CartId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
