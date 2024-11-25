using System;
using System.Collections.Generic;

namespace InventoryManagement
{
    // Class to represent a Product
    public class Product
    {
        public required string Name { get; set; }  // 'required' for C# 9.0 and above
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    // This is the class that contains the Main entry point of the program
    public class Program
    {
        // Function to add a new product to the inventory
        static void AddProduct(Dictionary<int, Product> inventory, int productID, string name, int quantity, double price)
        {
            Product newProduct = new Product { Name = name, Quantity = quantity, Price = price };
            inventory[productID] = newProduct;
            Console.WriteLine($"Product added: {name}");
        }

        // Function to remove a product from the inventory
        static void RemoveProduct(Dictionary<int, Product> inventory, int productID)
        {
            if (inventory.ContainsKey(productID))
            {
                inventory.Remove(productID);
                Console.WriteLine("Product removed successfully!");
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }

        // Function to update the quantity of a product
        static void UpdateProductQuantity(Dictionary<int, Product> inventory, int productID, int newQuantity)
        {
            if (inventory.ContainsKey(productID))
            {
                inventory[productID].Quantity = newQuantity;
                Console.WriteLine("Product quantity updated!");
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }

        // Function to display all products in the inventory
        static void DisplayInventory(Dictionary<int, Product> inventory)
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
                Console.WriteLine($"ID: {entry.Key}, Name: {product.Name}, Quantity: {product.Quantity}, Price: ${product.Price}");
            }
        }

        // Safe helper method to read and parse an integer from Console
        static int ReadIntFromConsole()
        {
            string? input = Console.ReadLine();  // Nullable string
            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int result))
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
                return -1; // You could return any default or error value, depending on your needs
            }
            return result;
        }

        // Safe helper method to read and parse a double from Console
        static double ReadDoubleFromConsole()
        {
            string? input = Console.ReadLine();  // Nullable string
            if (string.IsNullOrEmpty(input) || !double.TryParse(input, out double result))
            {
                Console.WriteLine("Invalid input! Please enter a valid price.");
                return -1.0; // You could return any default or error value, depending on your needs
            }
            return result;
        }

        // Main entry point of the program
        public static void Main(string[] args)
        {
            // Dictionary to store products by their ID
            Dictionary<int, Product> inventory = new Dictionary<int, Product>();

            while (true)
            {
                // Menu for user interaction
                Console.WriteLine("\n-- Inventory Management System --");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. Update Product Quantity");
                Console.WriteLine("4. Display Inventory");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                int choice = ReadIntFromConsole();

                if (choice == 1)
                {
                    Console.Write("Enter product ID: ");
                    int id = ReadIntFromConsole();
                    Console.Write("Enter product name: ");
                    string name = Console.ReadLine() ?? "";  // Use null-coalescing operator to provide a default if null
                    if (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Product name cannot be empty.");
                        continue;
                    }
                    Console.Write("Enter product quantity: ");
                    int quantity = ReadIntFromConsole();
                    Console.Write("Enter product price: ");
                    double price = ReadDoubleFromConsole();

                    // Validate inputs before adding
                    if (id != -1 && quantity != -1 && price != -1 && !string.IsNullOrEmpty(name))
                    {
                        AddProduct(inventory, id, name, quantity, price);
                    }
                }
                else if (choice == 2)
                {
                    Console.Write("Enter product ID to remove: ");
                    int id = ReadIntFromConsole();

                    if (id != -1)
                    {
                        RemoveProduct(inventory, id);
                    }
                }
                else if (choice == 3)
                {
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
                    DisplayInventory(inventory);
                }
                else if (choice == 5)
                {
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
