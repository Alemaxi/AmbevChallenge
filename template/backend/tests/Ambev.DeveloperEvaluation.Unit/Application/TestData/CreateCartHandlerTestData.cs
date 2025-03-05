using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.Commands;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class CreateCartHandlerTestData
    {
        private static readonly Faker<CreateCartCommand> createCartHandlerFaker = new Faker<CreateCartCommand>()
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
        public static CreateCartCommand GenerateValidCommand()
        {
            return createCartHandlerFaker.Generate();
        }
    }
}
