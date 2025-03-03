using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Infrastructure.TestData
{
    public static class CartRepositoryTestData
    {
        // Faker para CartProduct
        private static readonly Faker<CartProduct> cartProductFaker = new Faker<CartProduct>()
            .RuleFor(cp => cp.ProductId, f => Guid.NewGuid())
            .RuleFor(cp => cp.Quantity, f => f.Random.Int(1, 10));

        // Faker para Cart
        private static readonly Faker<Cart> cartFaker = new Faker<Cart>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.UserId, f => f.Random.Int(1, 1000))
            .RuleFor(c => c.Date, f => f.Date.Past())
            .RuleFor(c => c.Products, f =>
                // Gera entre 1 e 5 itens para o carrinho
                cartProductFaker.Generate(f.Random.Int(1, 5))
            );

        /// <summary>
        /// Gera um carrinho válido com dados aleatórios.
        /// </summary>
        /// <returns>Instância da entidade Cart</returns>
        public static Cart GenerateValidCart()
        {
            return cartFaker.Generate();
        }

        /// <summary>
        /// Gera uma coleção de carrinhos válidos com dados aleatórios.
        /// </summary>
        /// <param name="count">Número de carrinhos a serem gerados</param>
        /// <returns>Lista de instâncias da entidade Cart</returns>
        public static IEnumerable<Cart> GenerateValidCarts(int count = 10)
        {
            return cartFaker.Generate(count);
        }

        /// <summary>
        /// Gera um item de carrinho válido com dados aleatórios.
        /// </summary>
        /// <returns>Instância da entidade CartProduct</returns>
        public static CartProduct GenerateValidCartProduct()
        {
            return cartProductFaker.Generate();
        }
    }
}
