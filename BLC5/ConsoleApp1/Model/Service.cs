using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    internal class Service : Item
    {
        public string description { get; set; }

        public Service() { }

        public Service(int id, string name, double price, string Description) : base(id, name, price)
        {
            description = Description;
        }

        public override string ToString()
        {
            return $"{base.ToString()} , Description : {description}";
        }
    }
}
