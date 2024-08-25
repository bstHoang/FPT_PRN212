using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    internal class Product : Item
    {
        public double weight { get; set; }

        public Product() { }

        public Product(int id, string name, double price, double Weight) : base(id, name, price)
        {
            weight = Weight;
        }

        public override string ToString()
        {

            return $"{base.ToString()} , Weight : {weight}";
        }
    }
}
