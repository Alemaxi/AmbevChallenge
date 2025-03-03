namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class ProductRating
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Product Assessment value
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Assessment quantity
        /// </summary>
        public int Count { get; set; }
    }
}
