using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Command;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public class UpdateSaleHandlerTestData
    {
        private static readonly Faker<UpdateSaleCommand> updateSaleCommandFaker = new Faker<UpdateSaleCommand>()
        .RuleFor(s => s.Id, f => Guid.NewGuid())
        .RuleFor(s => s.Items, f =>
        {
            var fakerSaleItem = new Faker<SaleItemCommand>()
                // Caso os campos Id e SaleId não sejam necessários na criação, pode-se omitir ou gerar valores padrão.
                .RuleFor(si => si.ProductId, f => f.Random.Guid())
                .RuleFor(si => si.Quantity, f => f.Random.Int(1, 10));

            // Gera de 1 a 5 itens aleatórios
            return fakerSaleItem.Generate(f.Random.Int(1, 5));
        });

        /// <summary>
        /// Gera um comando de criação de venda com dados válidos e aleatórios.
        /// </summary>
        /// <returns>Um CreateSaleCommand com as propriedades preenchidas.</returns>
        public static UpdateSaleCommand GenerateValidCommand()
        {
            return updateSaleCommandFaker.Generate();
        }
    }
}
