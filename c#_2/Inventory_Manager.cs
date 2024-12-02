using System;
using System.Collections.Generic;

namespace InventoryManagement
{
    // Enum to represent the status of a product in the inventory
    public enum ProductStatus
    {
        Available,
        OutOfStock,
        Discontinued
    }

    // Struct to hold product details with additional metadata
    public struct ProductDetails
    {
        public string Name;
        public int Quantity;
        public double Price;
        public ProductStatus Status;

        // Constructor for initializing a product
        public ProductDetails(string name, int quantity, double price, ProductStatus status)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Status = status;
        }
    }

    // Class to manage inventory actions
    public class Program
    {
        // Dictionary to store products by their ID
        static Dictionary<int, ProductDetails> inventory = new Dictionary<int, ProductDetails>();

        // Function to add a new product to the inventory
        static void AddProduct(Dictionary<int, ProductDetails> inventory, int productID, string name, int quantity, double price, ProductStatus status)
        {
            ProductDetails newProduct = new ProductDetails(name, quantity, price, status);
            inventory[productID] = newProduct; // Add or update product
            Console.WriteLine($"Product added: {name} (ID: {productID})");
        }

        // Function to remove a product from the inventory
        static void RemoveProduct(Dictionary<int, ProductDetails> inventory, int productID)
        {
            if (inventory.ContainsKey(productID))
            {
                inventory.Remove(productID); // Remove product by ID
                Console.WriteLine("Product removed successfully!");
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }

        // Function to update the quantity of a product
        static void UpdateProductQuantity(Dictionary<int, ProductDetails> inventory, int productID, int newQuantity)
        {
            if (inventory.ContainsKey(productID))
            {
                var product = inventory[productID];
                product.Quantity = newQuantity; // Update product quantity
                inventory[productID] = product; // Reassign updated product
                Console.WriteLine("Product quantity updated!");
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }

        // Function to update the status of a product
        static void UpdateProductStatus(Dictionary<int, ProductDetails> inventory, int productID, ProductStatus newStatus)
        {
            if (inventory.ContainsKey(productID))
            {
                var product = inventory[productID];
                product.Status = newStatus; // Update product status
                inventory[productID] = product; // Reassign updated product
                Console.WriteLine($"Product status updated to {newStatus}.");
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }

        // Function to display all products in the inventory
        static void DisplayInventory(Dictionary<int, ProductDetails> inventory)
        {
            if (inventory.Count == 0)
            {
                Console.WriteLine("Inventory is empty!");
                return;
            }

            Console.WriteLine("Inventory List:");
            foreach (var entry in inventory)
            {
                var product = entry.Value;
                Console.WriteLine($"ID: {entry.Key}, Name: {product.Name}, Quantity: {product.Quantity}, Price: ${product.Price}, Status: {product.Status}");
            }
        }

        // Helper method to read an integer from the console safely
        static int ReadIntFromConsole()
        {
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int result))
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
                return -1;
            }
            return result;
        }

        // Helper method to read a double from the console safely
        static double ReadDoubleFromConsole()
        {
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || !double.TryParse(input, out double result))
            {
                Console.WriteLine("Invalid input! Please enter a valid price.");
                return -1.0;
            }
            return result;
        }

        // Helper method to read and parse product status from console
        static ProductStatus ReadProductStatusFromConsole()
        {
            Console.WriteLine("Select product status:");
            Console.WriteLine("1. Available");
            Console.WriteLine("2. OutOfStock");
            Console.WriteLine("3. Discontinued");
            int statusChoice = ReadIntFromConsole();

            return statusChoice switch
            {
                1 => ProductStatus.Available,
                2 => ProductStatus.OutOfStock,
                3 => ProductStatus.Discontinued,
                _ => ProductStatus.Available
            };
        }

        // Main entry point of the program
        public static void Main(string[] args)
        {
            while (true)
            {
                // Display main menu
                Console.WriteLine("\n-- Inventory Management System --");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. Update Product Quantity");
                Console.WriteLine("4. Update Product Status");
                Console.WriteLine("5. Display Inventory");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                int choice = ReadIntFromConsole();

                if (choice == 1)
                {
                    // Add a new product
                    Console.Write("Enter product ID: ");
                    int id = ReadIntFromConsole();
                    Console.Write("Enter product name: ");
                    string name = Console.ReadLine() ?? "";
                    if (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Product name cannot be empty.");
                        continue;
                    }
                    Console.Write("Enter product quantity: ");
                    int quantity = ReadIntFromConsole();
                    Console.Write("Enter product price: ");
                    double price = ReadDoubleFromConsole();
                    ProductStatus status = ReadProductStatusFromConsole();

                    // Validate inputs before adding
                    if (id != -1 && quantity != -1 && price != -1 && !string.IsNullOrEmpty(name))
                    {
                        AddProduct(inventory, id, name, quantity, price, status);
                    }
                }
                else if (choice == 2)
                {
                    // Remove a product
                    Console.Write("Enter product ID to remove: ");
                    int id = ReadIntFromConsole();
                    if (id != -1)
                    {
                        RemoveProduct(inventory, id);
                    }
                }
                else if (choice == 3)
                {
                    // Update product quantity
                    Console.Write("Enter product ID to update quantity: ");
                    int id = ReadIntFromConsole();
                    Console.Write("Enter new quantity: ");
                    int newQuantity = ReadIntFromConsole();
                    if (id != -1 && newQuantity != -1)
                    {
                        UpdateProductQuantity(inventory, id, newQuantity);
                    }
                }
                else if (choice == 4)
                {
                    // Update product status
                    Console.Write("Enter product ID to update status: ");
                    int id = ReadIntFromConsole();
                    if (id != -1)
                    {
                        ProductStatus newStatus = ReadProductStatusFromConsole();
                        UpdateProductStatus(inventory, id, newStatus);
                    }
                }
                else if (choice == 5)
                {
                    // Display all products in the inventory
                    DisplayInventory(inventory);
                }
                else if (choice == 6)
                {
                    // Exit program
                    Console.WriteLine("Exiting program.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice, please try again.");
                }
            }
        }
    }
}
