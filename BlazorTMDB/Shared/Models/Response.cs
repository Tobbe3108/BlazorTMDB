using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTMDB.Shared.Models
{
    public class Response
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public string query { get; set; }
        public List<Result> results { get; set; }
    }
}