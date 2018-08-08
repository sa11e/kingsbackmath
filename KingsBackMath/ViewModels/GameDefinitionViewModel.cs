using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingsBackMath.Data.Entities;

namespace KingsBackMath.ViewModels
{
    public class GameDefinitionViewModel
    {
        public int Id { get; set; }
        public GameDefintionType Type { get; set; }
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; }
        public int Rounds { get; set; }
        public GameDefintionStatus Status { get; set; }
    }
}
