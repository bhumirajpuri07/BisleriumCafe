using Newtonsoft.Json;

using BisleriumCafe.Models;
using BisleriumCafe.Utils;

namespace BisleriumCafe.Services
{
    /// <summary>
    /// Service for managing add-in-related operations, including saving, retrieving, and editing add-in data.
    /// </summary>
    public class AddInService
    {
        /// <summary>
        /// Saves the provided list of add-ins to a JSON file.
        /// </summary>
        /// <param name="addIns">List of add-ins to be saved.</param>
        public static void SaveAddInToJson(List<AddIn> addIns)
        {
            string filePath = PathManager.GetJSONFilePath("addins.json");
            string jsonData = JsonConvert.SerializeObject(addIns, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        /// <summary>
        /// Initializes add-in data by loading from the JSON file or creating sample data if the file is empty.
        /// </summary>
        public static void InjectAddInData()
        {
            string filePath = PathManager.GetJSONFilePath("addins.json");
            var existingData = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(existingData))
            {
                // Create sample add-in data if the file is empty
                List<AddIn> sampleAddIns = new List<AddIn>
                {
                    new AddIn { Name = "Honey", Price = 1.99M },
                    new AddIn { Name = "Cocoa Powder", Price = 2.39M },
                    new AddIn { Name = "Cinnamon", Price = 4.59M },
                    new AddIn { Name = "Vanilla", Price = 3.99M },
                    new AddIn { Name = "Ginger", Price = 1.55M },
                    new AddIn { Name = "Mint", Price = 5.59M },
                    new AddIn { Name = "Maple", Price = 0.99M },
                };
                SaveAddInToJson(sampleAddIns);
            }
        }

        /// <summary>
        /// Retrieves all add-in items from the JSON file.
        /// </summary>
        /// <returns>List of add-in items.</returns>
        public static List<AddIn> GetAllAddIn()
        {
            string filePath = PathManager.GetJSONFilePath("addins.json");
            try
            {
                string existingJsonData = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(existingJsonData))
                {
                    return new List<AddIn>();
                }
                return JsonConvert.DeserializeObject<List<AddIn>>(existingJsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return new List<AddIn>();
            }
        }

        /// <summary>
        /// Retrieves a specific add-in item by its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the add-in item.</param>
        /// <returns>Add-in item with the specified identifier.</returns>
        public static AddIn GetAddInById(Guid id)
        {
            List<AddIn> addIns = GetAllAddIn();
            return addIns.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Edits a specific add-in item with the provided information and saves the changes to the JSON file.
        /// </summary>
        /// <param name="id">Unique identifier of the add-in item to be edited.</param>
        /// <param name="newName">New name for the add-in item.</param>
        /// <param name="newPrice">New price for the add-in item.</param>
        /// <returns>List of add-in items after the edit operation.</returns>
        public static List<AddIn> EditAddIn(Guid id, string newName, decimal newPrice)
        {
            List<AddIn> addIns = GetAllAddIn();
            AddIn addInToEdit = addIns.FirstOrDefault(x => x.Id == id);

            if (addInToEdit == null)
            {
                throw new Exception("AddIn not found");
            }

            // Update add-in information
            addInToEdit.Name = newName;
            addInToEdit.Price = newPrice;

            // Save changes to the JSON file
            SaveAddInToJson(addIns);

            return addIns;
        }
    }
}
