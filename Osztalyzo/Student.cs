using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osztalyzo
{
    public class Student
    {
        public string name { get;private set; }
        public List<int> grades { get; set; }
        public float Average()
        {
            
                if (grades.Count == 0) return 0;
                return (float)grades.Sum() / grades.Count;
            
        }
        public Student(string name)
        {
            this.name = name;
            grades = new List<int>();
        }
    }
}
