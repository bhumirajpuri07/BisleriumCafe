using Newtonsoft.Json;

using BisleriumCafe.Models;
using BisleriumCafe.Utils;

namespace BisleriumCafe.Services
{
    /// <summary>
    /// Service for managing coffee-related operations, including saving, retrieving, and editing coffee data.
    /// </summary>
    public class CoffeeService
    {
        /// <summary>
        /// Saves the provided list of coffee to a JSON file.
        /// </summary>
        /// <param name="coffee">List of coffee items to be saved.</param>
        public static void SaveCoffeeToJson(List<Coffee> coffee)
        {
            string filePath = PathManager.GetJSONFilePath("coffee.json");
            string jsonData = JsonConvert.SerializeObject(coffee, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        /// <summary>
        /// Initializes coffee data by loading from the JSON file or creating sample data if the file is empty.
        /// </summary>
        public static void InjectCoffeeData()
        {
            string filePath = PathManager.GetJSONFilePath("coffee.json");
            var existingData = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(existingData))
            {
                // Create sample coffee data if the file is empty
                List<Coffee> sampleCoffee = new List<Coffee>
                {
                    new Coffee { Name = "Cappuccino", Price = 4.59M },
                    new Coffee { Name = "Mocha", Price = 4.99M },
                    new Coffee { Name = "Macchiato", Price = 4.79M },
                    new Coffee { Name = "Latte", Price = 4.49M },
                    new Coffee { Name = "Java", Price = 6.99M },
                    new Coffee { Name = "Americano", Price = 6.99M },
                    new Coffee { Name = "Espresso", Price = 5.99M },
                };
                SaveCoffeeToJson(sampleCoffee);
            }
        }

        /// <summary>
        /// Retrieves all coffee items from the JSON file.
        /// </summary>
        /// <returns>List of coffee items.</returns>
        public static List<Coffee> GetAllCoffee()
        {
            string filePath = PathManager.GetJSONFilePath("coffee.json");
            try
            {
                string existingJsonData = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(existingJsonData))
                {
                    return new List<Coffee>();
                }
                return JsonConvert.DeserializeObject<List<Coffee>>(existingJsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return new List<Coffee>();
            }
        }

        /// <summary>
        /// Retrieves a specific coffee item by its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the coffee item.</param>
        /// <returns>Coffee item with the specified identifier.</returns>
        public static Coffee GetCoffeeById(Guid id)
        {
            List<Coffee> coffeeList = GetAllCoffee();
            return coffeeList.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Edits a specific coffee item with the provided information and saves the changes to the JSON file.
        /// </summary>
        /// <param name="id">Unique identifier of the coffee item to be edited.</param>
        /// <param name="newName">New name for the coffee item.</param>
        /// <param name="newPrice">New price for the coffee item.</param>
        /// <returns>List of coffee items after the edit operation.</returns>
        public static List<Coffee> EditCoffee(Guid id, string newName, decimal newPrice)
        {
            List<Coffee> coffeeList = GetAllCoffee();
            Coffee coffeeToEdit = coffeeList.FirstOrDefault(x => x.Id == id);

            if (coffeeToEdit == null)
            {
                throw new Exception("Coffee not found");
            }

            // Update coffee information
            coffeeToEdit.Name = newName;
            coffeeToEdit.Price = newPrice;

            // Save changes to the JSON file
            SaveCoffeeToJson(coffeeList);

            return coffeeList;
        }
    }
}
