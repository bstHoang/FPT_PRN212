using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigFile2.Model
{
    internal class StudentPRN212 : User
    {
        public string Major { get; set; }
        public double Pt1 { get; set; } //5

        public double Pt2 { get; set; } //5

        public double Ass1 { get; set; } //10

        public double Ass2 { get; set; } //10

        public double Project { get; set; } //25

        public double Pe { get; set; } //25

        public double Fe { get; set; }//20

        public StudentPRN212() { }

        public StudentPRN212(int id, string name, string major, double pt1, double pt2, double ass1, double ass2, double project, double pe, double fe) : base(id, name)
        {
            Major = major;
            Pt1 = pt1;
            Pt2 = pt2;
            Ass1 = ass1;
            Ass2 = ass2;
            Project = project;
            Pe = pe;
            Fe = fe;
        }

        public double Total => Pt1 * 0.05 + Pt2 * 0.05 + Ass1 * 0.10 + Ass2 * 0.10 + Project * 0.25 + Pe * 0.25 + Fe * 0.20;

        public string PassStatus
        {
            get
            {
                if (Pt1 + Pt2 == 0 && Ass1 + Ass2 == 0 && Project == 0 && Pe == 0 && Fe == 0)
                    return "notPass";
                if (Total < 5)
                    return "notPass";
                return "Pass";
            }
        }

        public void Display() {
            Console.WriteLine(ToString());
        }
        public override string ToString()
        {
            return $"{base.ToString()} , Major : {Major} | Pt1: {Pt1} | Pt2: {Pt2} | Ass1: {Ass1} | Ass2: {Ass2} | Project: {Project} | PE: {Pe} | FE: {Fe} | Total: {Total} | {PassStatus}";
        }
    }
}
