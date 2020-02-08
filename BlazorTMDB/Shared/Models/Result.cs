using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTMDB.Shared.Models
{
    public interface Result
    {
        public int id { get; set; }
        public double popularity { get; set; }
        public string posterPath { get; set; }
    }
}