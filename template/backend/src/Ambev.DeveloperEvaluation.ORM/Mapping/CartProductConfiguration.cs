using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            // Define o nome da tabela
            builder.ToTable("CartProducts");

            // Define a chave composta (CartId e ProductId)
            builder.HasKey(cp => new { cp.CartId, cp.ProductId });

            // Propriedade Quantity como obrigatória
            builder.Property(cp => cp.Quantity)
                   .IsRequired();
        }
    }
}
