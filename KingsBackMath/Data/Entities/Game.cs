using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingsBackMath.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime TimeCreated { get; set; }
        public GameUser User { get; set; }
        public int GameDefinitionId { get; set; }
        public int TimeSecs { get; set; }
        public int Score { get; set; }

        public virtual  GameDefinition GameDefinition { get; set; }
    }
}
