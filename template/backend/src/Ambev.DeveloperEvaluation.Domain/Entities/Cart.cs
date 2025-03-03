using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Cart : IEntity
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CartProduct> Products { get; set; } = new List<CartProduct>();
    }
}
