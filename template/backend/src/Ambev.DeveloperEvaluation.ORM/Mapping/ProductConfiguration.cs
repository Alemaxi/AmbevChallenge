using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Define o nome da tabela
            builder.ToTable("Products");

            // Define a chave primária (assumindo que BaseEntity possui a propriedade Id)
            builder.HasKey(p => p.Id);

            // Configura a propriedade Title como obrigatória e com tamanho máximo
            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            // Configura a propriedade Price como obrigatória e define o tipo decimal com precisão
            builder.Property(p => p.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Configura a propriedade Description com tamanho máximo (opcional, ajuste conforme necessário)
            builder.Property(p => p.Description)
                   .HasMaxLength(1000);

            // Configura a propriedade Category como obrigatória e com tamanho máximo
            builder.Property(p => p.Category)
                   .IsRequired()
                   .HasMaxLength(100);

            // Configura a propriedade Image com tamanho máximo (pode ser opcional)
            builder.Property(p => p.Image)
                   .HasMaxLength(500);

            // Configura a propriedade CreatedAt como obrigatória
            builder.Property(p => p.CreatedAt)
                   .IsRequired();

            // Configura a propriedade UpdatedAt (opcional)
            builder.Property(p => p.UpdatedAt);

            // Configura a propriedade Rating como um tipo 'owned'
            builder.OwnsOne(p => p.Rating, r =>
            {
                // Configura a propriedade Rate da avaliação
                r.Property(rating => rating.Rate)
                 .IsRequired()
                 .HasColumnName("RatingRate")
                 .HasColumnType("decimal(5,2)");

                // Configura a propriedade Count da avaliação
                r.Property(rating => rating.Count)
                 .IsRequired()
                 .HasColumnName("RatingCount");
            });

        }
    }
}
