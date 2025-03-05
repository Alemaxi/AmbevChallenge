using Ambev.DeveloperEvaluation.Application.Carts.Shared.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public class UpdateCartHandlerTestData
    {
        private static readonly Faker<UpdateCartCommand> createCartHandlerFaker = new Faker<UpdateCartCommand>()
        .RuleFor(c => c.UserId, f => f.Random.Int(1, 1000))
        .RuleFor(c => c.Date, f => f.Date.Recent())
        .RuleFor(c => c.Products, f =>
        {
            var fakerCartProduct = new Faker<CartProductCommand>()
                .RuleFor(cp => cp.ProductId, f => f.Random.Guid())
                .RuleFor(cp => cp.Quantity, f => f.Random.Int(1, 10));
            // Gera de 1 a 5 produtos aleatórios
            return fakerCartProduct.Generate(f.Random.Int(1, 5));
        });

        /// <summary>
        /// Gera um comando de criação de carrinho com dados válidos e aleatórios.
        /// </summary>
        /// <returns>Um CreateCartCommand com propriedades preenchidas.</returns>
        public static UpdateCartCommand GenerateValidCommand()
        {
            return createCartHandlerFaker.Generate();
        }
    }
}
