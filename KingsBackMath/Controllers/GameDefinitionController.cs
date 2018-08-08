using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KingsBackMath.Data;
using KingsBackMath.Data.Entities;
using KingsBackMath.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KingsBackMath.Controllers
{
    [Produces("application/json")]
    [Route("api/GameDefinition")]
    public class GameDefinitionController : Controller
    {
        private readonly IKingsBackMathRepository repository;
        private readonly ILogger<GamesController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<GameUser> userManager;

        public GameDefinitionController(IKingsBackMathRepository repository,
            ILogger<GamesController> logger,
            IMapper mapper,
            UserManager<GameUser> userManager)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GameDefinitionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newGameDefinition = mapper.Map<GameDefinitionViewModel, GameDefinition>(model);
                    var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
                    newGameDefinition.CreatedBy = currentUser;
                    newGameDefinition.TimeCreated = DateTime.Now;
                    repository.AddEntity(newGameDefinition);

                    if (repository.SaveAll())
                        return Created($"/api/GameDefinition({model.Id}", model);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to save game definition. {e}");
            }
            return BadRequest("Failed to save game definition");
        }
    }
}