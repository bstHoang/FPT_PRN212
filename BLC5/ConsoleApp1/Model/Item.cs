using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    internal class Item
    {
        public int id { get; set; }
        public string name { get; set; }

        public double price { get; set; }

        public Item() { }

        public Item(int id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        public override string ToString()
        {
            return $"ID : {id} , Name : {name} , Price : {price}";
        }
    }
}
