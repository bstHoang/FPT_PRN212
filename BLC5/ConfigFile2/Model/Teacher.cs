using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigFile2.Model
{
    internal class Teacher : User
    {
        public string Skill { get; set; }
        public Teacher() { }

        public Teacher(int id , string name ,string skill) : base (id , name)
        {
            Skill = skill;
        }

        public override string ToString()
        {
            return $"{base.ToString()} , Skill : {Skill}";
        }
    }
}
