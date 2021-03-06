﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingsBackMath.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KingsBackMath.Data
{
    public class KingsBackMathRepository : IKingsBackMathRepository
    {
        private readonly KingsBackMathContext context;
        private readonly ILogger<KingsBackMathRepository> logger;

        public KingsBackMathRepository(KingsBackMathContext context, ILogger<KingsBackMathRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public string GetConnectionString()
        {
            return context?.Database?.GetDbConnection()?.ConnectionString;
        }

        public int TestDb()
        {
            try
            {
                logger.LogInformation("TestDb was called");
                return context.Games.Count();
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to get all games {e}");
                return -1;
            }
        }

        public IEnumerable<Game> GetAllGames()
        {
            try
            {
                logger.LogInformation("GetAllGames was called");
                return context.Games.OrderBy(g => g.Id);
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to get all games {e}");
                return null;
            }
        }
       
        public IEnumerable<Game> GetGamesByUser(string userName)
        {
            return context.Games.Where(g => g.User.UserName == userName).OrderBy(g => g.Id);
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }

        public Riddle GetRiddleOfToday()
        {
            var todaysRiddle = context.Riddles.FirstOrDefault(r => r.LastDisplayDate == DateTime.Now.Date);
            if (todaysRiddle != null)
                return todaysRiddle;

            todaysRiddle = context.Riddles.FirstOrDefault(r => r.LastDisplayDate == null);
            if (todaysRiddle == null)
            {
                // All riddles already displayed. Select an old one to display
                todaysRiddle = context.Riddles.OrderBy(r => r.LastDisplayDate).FirstOrDefault();
            }

            // Set this days as the last display date for the riddle
            if (todaysRiddle != null)
            {
                todaysRiddle.LastDisplayDate = DateTime.Now.Date;
                SaveAll();
            }

            return todaysRiddle;
        }

        public IEnumerable<Riddle> GetAllRiddles()
        {
            try
            {
                logger.LogInformation("GetAllRiddles was called");
                return context.Riddles.OrderBy(g => g.Id);
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to get all riddles: {e}");
                return null;
            }
        }

        public Riddle GetRiddle(int id)
        {
            return context.Riddles.FirstOrDefault(r => r.Id == id);
        }

        public void AddEntity(object model)
        {
            context.Add(model);
        }
    }
}
