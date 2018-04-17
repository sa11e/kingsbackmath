using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingsBackMath.Data;
using KingsBackMath.Services;
using KingsBackMath.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KingsBackMath.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;
        private readonly IKingsBackMathRepository repository;

        public AppController(IMailService mailService, IKingsBackMathRepository repository)
        {
            this.mailService = mailService;
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact us";
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Send email
                mailService.SendMessage(model.Email, model.Subject, model.Message);
                ViewBag.UserMessage = "Mail sent.";
                ModelState.Clear();
            }

            return View();
        }


        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }

        [Authorize]
        public IActionResult Games()
        {
            var games = repository.GetAllGames();
            return View(games);
        }
    }
}
