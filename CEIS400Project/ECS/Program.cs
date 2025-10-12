using System;
using System.Collections.Generic;
using ECS.Domain.Services;

namespace ECS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Seed initial data
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Alice" },
                new Employee { Id = 2, Name = "Bob" }
            };

            var equipment = new List<Equipment>
            {
                new Equipment { Id = 100, Name = "Laptop" },
                new Equipment { Id = 101, Name = "Projector" },
                new Equipment { Id = 102, Name = "Camera" }
            };

            // Initialize Repos and Services
            var checkoutRepo = new InMemoryCheckoutRepo();
            var checkoutService = new CheckoutService(checkoutRepo, employees, equipment);
            var reportService = new ReportService(checkoutRepo, employees, equipment);

            Console.WriteLine("=== Equipment Checkout System Demo ===\n");

            // 1. Check out one item
            Console.WriteLine("Alice checks out Laptop...");
            checkoutService.Checkout(1, 100);

            // 2. Check if a specific item is checked out
            Console.WriteLine("\nIs Laptop checked out? " + checkoutService.IsCheckedOut(100));
            Console.WriteLine("Is Projector checked out? " + checkoutService.IsCheckedOut(101));

            // 3. Display current status
            Console.WriteLine("\nChecked Out Items:");
            foreach (var msg in reportService.GetCheckedOutItems())
                Console.WriteLine(" - " + msg);

            Console.WriteLine("\nAvailable Items:");
            foreach (var msg in reportService.GetAvailableItems())
                Console.WriteLine(" - " + msg);

            // 4. Return the Laptop
            Console.WriteLine("\nAlice returns the Laptop...");
            checkoutService.Return(100);

            // 5. Display final status
            Console.WriteLine("\nChecked Out Items:");
            foreach (var msg in reportService.GetCheckedOutItems())
                Console.WriteLine(" - " + msg);

            Console.WriteLine("\nAvailable Items:");
            foreach (var msg in reportService.GetAvailableItems())
                Console.WriteLine(" - " + msg);

            Console.WriteLine("\n=== Demo complete ===");
            Console.ReadLine();
        }
    }
}
