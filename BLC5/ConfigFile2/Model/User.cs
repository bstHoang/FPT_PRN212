using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigFile2.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User() { }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"ID : {Id} , Name : {Name}";
        }
    }
}
