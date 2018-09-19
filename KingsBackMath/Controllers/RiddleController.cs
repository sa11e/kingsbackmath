using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KingsBackMath.Data;
using KingsBackMath.Data.Entities;
using KingsBackMath.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AzureStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KingsBackMath.Controllers
{
    //[Route("Riddle")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RiddleController : Controller
    {
        private readonly IKingsBackMathRepository repository;
        private readonly ILogger<RiddleController> logger;
        private readonly IMapper mapper;

        public RiddleController(IKingsBackMathRepository repository, ILogger<RiddleController> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var riddles = repository.GetAllRiddles();
                List<RiddleViewModel> result = new List<RiddleViewModel>();
                foreach (var riddle in riddles)
                {
                    var rvm = mapper.Map<Riddle, RiddleViewModel>(riddle);
                    result.Add(rvm);
                }
                return View(result);
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to get riddles. {e}");
                return BadRequest("Failed to get riddles");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var riddle = repository.GetRiddle(id);
            if (riddle == null)
                return NotFound();

            var model = mapper.Map<Riddle, RiddleViewModel>(riddle);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind] RiddleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newRiddle = mapper.Map<RiddleViewModel, Riddle>(model);
                    //var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
                    repository.AddEntity(newRiddle);
                    repository.SaveAll();
                    return RedirectToAction("Index");
                }
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to create riddle. {e}");
            }
            return BadRequest("Failed to create riddle");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit([Bind] RiddleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var riddle = repository.GetRiddle(model.Id);
                    if (riddle == null)
                        return NotFound();

                    logger.LogError($"Update riddle {model.Id}");
                    riddle.Question = model.Question;
                    riddle.Answer = model.Answer;
                    riddle.Difficulty = model.Difficulty;
                    riddle.Rank = model.Rank;

                    repository.SaveAll();
                    return RedirectToAction("Index");
                }

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to create riddle. {e}");
            }
            return BadRequest("Failed to create riddle");
        }



        [HttpGet]
        public IActionResult GetRiddleOfToday()
        {
            try
            {
                var riddleView = GetRiddleOfTodayRiddleViewModel();

                if (riddleView != null)
                {
                    return Ok(riddleView);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to get riddle. {e}");
                return BadRequest("Failed to get riddle");
            }
        }

        private RiddleViewModel GetRiddleOfTodayRiddleViewModel()
        {
            var riddle = repository.GetRiddleOfToday();
            RiddleViewModel riddleView = null;

            if (riddle != null)
                riddleView = mapper.Map<Riddle, RiddleViewModel>(riddle);
            return riddleView;
        }
    }
}