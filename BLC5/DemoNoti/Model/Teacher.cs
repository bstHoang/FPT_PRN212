using DemoNotif.Deligate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNotif.Model
{
    internal class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Teacher() { }

        public Teacher(int id, string name)
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
