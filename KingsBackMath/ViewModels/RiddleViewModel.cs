using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingsBackMath.ViewModels
{
    public class RiddleViewModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        // Display date. If set to todays date, use this one. Otherwise find one with null
        public DateTime LastDisplayDate { get; set; }
        public int Rank { get; set; }
        public int Difficulty { get; set; }
        public int Views { get; set; }
    }
}
