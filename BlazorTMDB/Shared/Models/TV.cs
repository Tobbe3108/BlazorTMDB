using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTMDB.Shared.Models
{
    public class TV : Result
    {
        public List<int> genreIds { get; set; }
        public string title { get; set; }
        public int voteCount { get; set; }
        public string posterPath { get; set; }
        public string backdropPath { get; set; }
        public string overview { get; set; }
        public int id { get; set; }
        public double popularity { get; set; }
    }
}