using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.Models
{
    public class GamesHistory
    {
        public DateTime GameStartTime { get; set; }

        public string WinnerEmail { get; set; }

        public string LoserEmail { get; set; }
        public DateTime GameFinishedTime { get; set; }
    }
}
