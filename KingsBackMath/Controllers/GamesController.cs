using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KingsBackMath.Data;
using KingsBackMath.Data.Entities;
using KingsBackMath.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KingsBackMath.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamesController : Controller
    {
        private readonly IKingsBackMathRepository repository;
        private readonly ILogger<GamesController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<GameUser> userManager;

        public GamesController(IKingsBackMathRepository repository, 
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
                return Ok(repository.GetGamesByUser(user));
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to get games. {e}");
                return BadRequest("Failed to get games");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var game = repository.GetAllGames().FirstOrDefault(g => g.Id == id);
                if (game != null)
                {
                    return Ok(mapper.Map<Game, GameViewModel>(game));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to get game with id {id}. {e}");
                return BadRequest("Failed to get game");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GameViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newGame = mapper.Map<GameViewModel, Game>(model);
                    var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
                    newGame.User = currentUser;
                    newGame.TimeCreated = DateTime.Now;
                    repository.AddEntity(newGame);

                    if (repository.SaveAll())
                        return Created($"/api/games({model.Id}", model);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to save game. {e}");
            }
            return BadRequest("Failed to save order");
        }
    }
}
