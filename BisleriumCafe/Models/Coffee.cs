using System.ComponentModel.DataAnnotations;

namespace BisleriumCafe.Models
{
    /// <summary>
    /// Represents a coffee item with properties like Id, Name, and Price.
    /// </summary>
    public class Coffee
    {
        /// <summary>
        /// Gets or sets the unique identifier for the coffee.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the name of the coffee.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the coffee.
        /// </summary>
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
    }
}
