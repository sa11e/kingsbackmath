using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KingsBackMath.Data;
using KingsBackMath.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KingsBackMath.Controllers
{
    [Route("api/[Controller]")]
    public class TestController : Controller
    {
        private readonly IKingsBackMathRepository repository;
        private readonly ILogger<GamesController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<GameUser> userManager;

        public TestController(IKingsBackMathRepository repository,
            ILogger<GamesController> logger,
            IMapper mapper,
            UserManager<GameUser> userManager)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var user = User.Identity.Name;
                var nr = repository.GetGamesByUser(user);
                return Ok(nr);
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to get games. {e}");
                return BadRequest("Failed to get games");
            }
        }

    }
}