using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Model
{
    internal class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<Item, int> ListItem { get; set; } = new Dictionary<Item, int>();
        public Order() { }
        public Order(int id, DateTime date)
        {
            Id = id;
            Date = date;
        }
        public void Display()
        {
            Console.WriteLine($"Order ID: {Id}");
            Console.WriteLine($"Date: {Date}");
            foreach (var entry in ListItem)
            {
                var item = entry.Key;
                var quantity = entry.Value;
                string description = item is Service service ? service.description : "N/A";
                double weight = item is Product product ? product.weight : 0.0;
                Console.WriteLine($"ID : {item.id},Item: {item.name}, Quantity: {quantity}, Price: {item.price}, Description: {description}, Weight: {weight}");
            }
            Console.WriteLine($"Total Amount : {GetTotalAmount()}");
            Console.WriteLine($"Total Weight : {GetTotalWeight()}");
        }

        public void AddItem(Item item)
        {
            AddItem(item, 1);
        }

        public void AddItem(Item item, int quantity)
        {
            if (ListItem.ContainsKey(item))
            {
                ListItem[item] += quantity;
            }
            else
            {
                ListItem[item] = quantity;
            }
        }

        public void Remove(Item item)
        {
            if (ListItem.ContainsKey(item))
            {
                ListItem.Remove(item);
            }
        }

        public void Remove(Item item, int quantity)
        {
            if (ListItem.ContainsKey(item))
            {
                ListItem[item] -= quantity;
                if (ListItem[item] <= 0)
                {
                    ListItem.Remove(item);
                }
            }
        }

        public double GetTotalAmount()
        {
            return ListItem.Sum(item => item.Key.price * item.Value);
        }

        public double GetTotalWeight()
        {
            return ListItem
                .Where(item => item.Key is Product)
                .Sum(item => ((Product)item.Key).weight * item.Value);
        }
    }
}
