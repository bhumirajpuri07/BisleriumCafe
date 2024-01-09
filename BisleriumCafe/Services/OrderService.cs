using Newtonsoft.Json;
using BisleriumCafe.Models;
using BisleriumCafe.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BisleriumCafe.Services
{
    public class OrderService
    {
        private static readonly string filePath = PathManager.GetJSONFilePath("orders.json");

        // Existing method to get all orders
        public static List<Order> GetAllOrder()
        {
            try
            {
                var json = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Order>();
                }

                return JsonConvert.DeserializeObject<List<Order>>(json);
            }
            catch (IOException ex)
            {
                // Handle exceptions related to file reading
                throw new Exception("An error occurred while reading the file.", ex);
            }
            catch (JsonException ex)
            {
                // Handle exceptions related to JSON deserialization
                throw new Exception("An error occurred while deserializing the JSON data.", ex);
            }
        }

        // Existing method to save an order to JSON
        public static void SaveOrderToJson(Order order)
        {
            try
            {
                var orders = GetAllOrder();
                orders.Add(order);

                var json = JsonConvert.SerializeObject(orders, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (IOException ex)
            {
                // Handle exceptions related to file writing
                throw new Exception("An error occurred while writing to the file.", ex);
            }
            catch (JsonException ex)
            {
                // Handle exceptions related to JSON serialization
                throw new Exception("An error occurred while serializing the JSON data.", ex);
            }
        }

        // New method to get orders by time range
        public static List<Order> GetOrdersByTimeRange(Func<DateTime, bool> filter)
        {
            try
            {
                var allOrders = GetAllOrder();

                // Apply the filter to get orders within the specified time range
                var filteredOrders = allOrders.Where(order => filter(order.OrderDate)).ToList();

                return filteredOrders;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while getting orders by time range.", ex);
            }
        }

        // New method to get the top 5 most selling coffees and quantities
        public static Dictionary<string, int> GetTopSellingCoffees()
        {
            try
            {
                var allOrders = GetAllOrder();

                var topCoffees = allOrders
                    .SelectMany(order => order.Coffee != null ? new[] { order.Coffee.Name } : Enumerable.Empty<string>())
                    .GroupBy(coffee => coffee)
                    .OrderByDescending(group => group.Count())
                    .Take(5)
                    .ToDictionary(group => group.Key, group => group.Count());

                return topCoffees;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while getting top selling coffees.", ex);
            }
        }

        // New method to get the top selling add-ins and quantities
        public static Dictionary<string, int> GetTopSellingAddIns()
        {
            try
            {
                var allOrders = GetAllOrder();

                var topAddIns = allOrders
                    .SelectMany(order => order.AddIns != null ? order.AddIns.Select(addIn => addIn.Name) : Enumerable.Empty<string>())
                    .GroupBy(addIn => addIn)
                    .OrderByDescending(group => group.Count())
                    .Take(5)
                    .ToDictionary(group => group.Key, group => group.Count());

                return topAddIns;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while getting top selling add-ins.", ex);
            }
        }

        public static decimal GetTotalRevenue()
        {
            try
            {
                var allOrders = GetAllOrder();

                // Calculate total revenue by summing up the total prices of all orders
                decimal totalRevenue = allOrders.Sum(order => order.TotalPrice);

                return totalRevenue;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while calculating total revenue.", ex);
            }
        }
    }
}