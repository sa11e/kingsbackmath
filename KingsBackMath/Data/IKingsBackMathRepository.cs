using System.Collections.Generic;
using KingsBackMath.Data.Entities;

namespace KingsBackMath.Data
{
    public interface IKingsBackMathRepository
    {
        IEnumerable<Game> GetAllGames();
        
        IEnumerable<Game> GetGamesByUser(string userName);
        bool SaveAll();

        Riddle GetRiddleOfToday();
        IEnumerable<Riddle> GetAllRiddles();

        Riddle GetRiddle(int id);

        void AddEntity(object model);
        string GetConnectionString();
    }
}