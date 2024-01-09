using System.ComponentModel.DataAnnotations;

namespace BisleriumCafe.Models
{
    /// <summary>
    /// Represents an order with customer information, selected coffee, add-ins, total price, and order date.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Initializes a new instance of the Order class with the current date and an empty Coffee.
        /// </summary>
        public Order()
        {
            OrderDate = DateTime.Now;
            Coffee = new Coffee();
        }

        /// <summary>
        /// Gets or sets the customer's name for the order.
        /// </summary>
        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the customer's phone number for the order.
        /// </summary>
        [Required(ErrorMessage = "Customer Phone Number is required.")]
        public string CustomerPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the selected coffee for the order.
        /// </summary>
        [Required(ErrorMessage = "Coffee is required.")]
        public Coffee Coffee { get; set; }

        /// <summary>
        /// Gets or sets the list of add-ins for the coffee in the order.
        /// </summary>
        public List<AddIn> AddIns { get; set; } = new List<AddIn>();

        /// <summary>
        /// Gets the total price of the order, including the coffee and add-ins.
        /// </summary>
        public decimal TotalPrice => CalculateTotalPrice();

        /// <summary>
        /// Gets the date and time when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; private set; }

        /// <summary>
        /// Calculates the total price of the order, including the coffee and add-ins.
        /// </summary>
        /// <returns>Total price of the order.</returns>
        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = Coffee?.Price ?? 0;

            foreach (var addIn in AddIns)
            {
                totalPrice += addIn.Price;
            }

            return totalPrice;
        }
    }
}
