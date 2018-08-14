using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KingsBackMath.Data;
using KingsBackMath.Data.Entities;
using KingsBackMath.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KingsBackMath.Controllers
{
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

        public IActionResult Index()
        {
            var riddleView = GetOfToday();
            return View(riddleView);
        }

        [HttpGet]
        public IActionResult GetRiddleOfToday()
        {
            try
            {
                var riddleView = GetOfToday();

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

        private RiddleViewModel GetOfToday()
        {
            var riddle = repository.GetRiddleOfToday();
            RiddleViewModel riddleView = null;

            if (riddle != null)
                riddleView = mapper.Map<Riddle, RiddleViewModel>(riddle);
            return riddleView;
        }
    }
}