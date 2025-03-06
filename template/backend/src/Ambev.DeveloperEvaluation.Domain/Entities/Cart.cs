using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CartProduct> Products { get; set; } = new List<CartProduct>();

        // Propriedade de navegação para referenciar o usuário
        public User User { get; set; }
    }
}
