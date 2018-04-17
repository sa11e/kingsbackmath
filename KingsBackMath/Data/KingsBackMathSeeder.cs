﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingsBackMath.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace KingsBackMath.Data
{
    public class KingsBackMathSeeder
    {
        private readonly KingsBackMathContext context;
        private readonly IHostingEnvironment hosting;
        private readonly UserManager<GameUser> userManager;

        public KingsBackMathSeeder(KingsBackMathContext context, 
            IHostingEnvironment hosting,
            UserManager<GameUser> userManager)
        {
            this.context = context;
            this.hosting = hosting;
            this.userManager = userManager;
        }

        public async Task Seed()
        {
            context.Database.EnsureCreated();

            var user = await userManager.FindByEmailAsync("esa@else.se");
            if (user == null)
            {
                user = new GameUser
                {
                    FirstName = "Erik",
                    LastName = "Sahlmén",
                    UserName = "esa@else.se",
                    Email = "esa@else.se"
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            }

            if (!context.Games.Any())
            {
                // Seed some games
                context.Games.Add(new Game
                {
                    User = user,
                    Rounds = 3,
                    Score = 111,
                    TimeSecs = 3
                });
                context.SaveChanges();
            }
        }
    }
}
