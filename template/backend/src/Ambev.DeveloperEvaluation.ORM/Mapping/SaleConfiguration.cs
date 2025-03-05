using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            // Chave primária
            builder.HasKey(s => s.Id);

            // Data da venda é obrigatória
            builder.Property(s => s.SaleDate)
                .IsRequired();

            // Filial
            builder.Property(s => s.Branch)
                .IsRequired()
                .HasMaxLength(100);

            // Indicador de cancelamento
            builder.Property(s => s.IsCancelled)
                .IsRequired();

            // Relacionamento com Customer (User)
            builder.HasOne(s => s.Customer)
                .WithMany() // Caso o User não possua uma coleção de Sales
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com os itens da venda
            builder.HasMany(s => s.Items)
                .WithOne(si => si.Sale)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
