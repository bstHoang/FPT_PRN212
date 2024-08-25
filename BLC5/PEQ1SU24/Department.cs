using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_SU24_Q1
{
    internal class Department<T>
    {
        public string Title { get; set; }
        private List<T> items;

        public Department(string title)
        {
            Title = title;
            items = new List<T>();
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        // Use the delegate from the PresentationDelegate class
        public void Display(Presentation<T> presentation)
        {
            Console.WriteLine($"Department '{Title}' has {items.Count} employees. List of employees:");
            foreach (T item in items)
            {
                presentation(item);
            }
        }
    }
}
