using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "1prn1.txt";
            List<Order> orders = ReadOrdersFromFile(filePath);
            foreach (var order in orders)
            {
                order.Display();
                Console.WriteLine();
            }
        }

        static List<Order> ReadOrdersFromFile(string filePath)
        {
            List<Order> orders = new List<Order>();
            Order order = null;

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("Order ID:"))
                    {
                        if (order != null)
                        {
                            orders.Add(order);
                        }
                        order = new Order();
                        order.Id = ParseInt(line.Substring("Order ID:".Length).Trim());
                    }
                    else if (line.StartsWith("Date:"))
                    {
                        order.Date = ParseDateTime(line.Substring("Date:".Length).Trim());
                    }
                    else if (line.StartsWith("ID :"))
                    {
                        var parts = line.Split(new[] { ',', ':' }, StringSplitOptions.RemoveEmptyEntries);
                        int id = ParseInt(parts.Length > 1 ? parts[1].Trim() : "0");
                        string itemName = parts.Length > 3 ? parts[3].Trim() : "N/A";
                        int quantity = ParseInt(parts.Length > 5 ? parts[5].Trim() : "0");
                        double price = ParseDouble(parts.Length > 7 ? parts[7].Trim() : "0.0");
                        string description = parts.Length > 9 ? parts[9].Trim() : "N/A";
                        double weight = ParseDouble(parts.Length > 11 ? parts[11].Trim() : "0.0");

                        Item item;
                        if (description != "N/A")
                        {
                            item = new Service(id, itemName, price, description);
                        }
                        else if (weight != 0.0)
                        {
                            item = new Product(id, itemName, price, weight);
                        }
                        else
                        {
                            item = new Item(id, itemName, price);
                        }

                        order.AddItem(item, quantity);
                    }
                }
                if (order != null)
                {
                    orders.Add(order);
                }
            }
            return orders;
        }

        static int ParseInt(string input)
        {
            return int.TryParse(input, out int value) ? value : 0;
        }

        static double ParseDouble(string input)
        {
            return double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double value) ? value : 0.0;
        }

        static DateTime ParseDateTime(string input)
        {
            return DateTime.TryParse(input, out DateTime value) ? value : DateTime.MinValue;
        }
    }
}