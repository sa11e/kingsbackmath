using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KingsBackMath.Data.Entities
{
    public class GameDefinition
    {
        public int Id { get; set; }
        public GameDefintionType Type { get; set; }
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; }
        public int Rounds { get; set; }
        public GameDefintionStatus Status { get; set; }
        public DateTime TimeCreated { get; set; }
        public GameUser CreatedBy { get; set; }
    }

    public enum GameDefintionType
    {
        Addition = 10,
        Substracion = 20
    }
    public enum GameDefintionStatus
    {
        Active = 10,
        InActive = 20
    }

}
