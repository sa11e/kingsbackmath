using System.Collections.Generic;
using KingsBackMath.Data.Entities;

namespace KingsBackMath.Data
{
    public interface IKingsBackMathRepository
    {
        IEnumerable<Game> GetAllGames();
        IEnumerable<Game> GetGamesByUser(string userName);
        bool SaveAll();
        
        void AddEntity(object model);
        string GetConnectionString();
    }
}