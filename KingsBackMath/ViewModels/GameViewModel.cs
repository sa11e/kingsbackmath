using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingsBackMath.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public DateTime TimeCreated { get; set; }
        public int UserId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int Rounds { get; set; }

        [Range(1, Int32.MaxValue)]
        public int TimeSecs { get; set; }

        [Range(1, Int32.MaxValue)]
        public int Score { get; set; }
    }
}
