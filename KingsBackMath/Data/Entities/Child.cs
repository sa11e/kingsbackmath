using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingsBackMath.Data.Entities
{
    public class Child
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int GameUserId { get; set; }
        public GameUser GameUser { get; set; }
    }
}
