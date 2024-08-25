using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeligateTask.Model
{
    internal class Grade
    {
        public int Mark { get; set; }
        public double Rate { get; set; }

        public Grade() { }

        public Grade(int mark, double rate)
        {
            Mark = mark;
            Rate = rate;
        }

        public override string ToString()
        {
            return $"Mark : {Mark} , Rate : {Rate}";
        }
    }
}
