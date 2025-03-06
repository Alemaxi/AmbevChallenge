using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Command;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public class CreateSaleHandlerTestData
    {
        private static readonly Faker<CreateSaleCommand> createSaleCommandFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.SaleDate, f => f.Date.Recent())
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.Branch, f => f.Commerce.Department())
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
        public static CreateSaleCommand GenerateValidCommand()
        {
            return createSaleCommandFaker.Generate();
        }
    }
}
