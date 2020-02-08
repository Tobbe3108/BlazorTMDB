using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTMDB.Shared.Models
{
    public class Person : Result
    {
        public string name { get; set; }
        public string job { get; set; }
        public string posterPath { get; set; }
        public int id { get; set; }
        public double popularity { get; set; }
        public List<int> knownFor { get; set; }
    }
}