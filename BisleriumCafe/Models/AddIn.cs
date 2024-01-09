using System.ComponentModel.DataAnnotations;

namespace BisleriumCafe.Models
{
    /// <summary>
    /// Represents an add-in item with properties like Id, Name, and Price.
    /// </summary>
    public class AddIn
    {
        /// <summary>
        /// Gets or sets the unique identifier for the add-in.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the name of the add-in.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the add-in.
        /// </summary>
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
    }
}
