using System;
using System.Collections.Generic;

namespace Project.Model
{
    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public Dictionary<string, double?> Scores { get; set; } = new Dictionary<string, double?>();
        public double GPA { get; set; }

        public void SetScore(string key, double? value)
        {
            if (value.HasValue && (value.Value < 0 || value.Value > 10))
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Score must be between 0 and 10.");
            }

            Scores[key] = value;
        }
    }
}
