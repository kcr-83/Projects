using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Core;
using GameShop.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IGameBuyingRequestProcessor _processor;

        public HomeController(IGameBuyingRequestProcessor processor)
        {
            _processor = processor;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BuyGame(GameBuyModel buyGame)
        {
            var request = new GameBuyingRequest()
            {
                Email = buyGame.Email,
                FirstName = buyGame.FirstName,
                LastName = buyGame.LastName,
            };
            request.Date = DateTime.Now;
            request.GameToBuy = new Game()
            {
                Id = buyGame.Game.Id,
                Name = buyGame.Game.Name
            };
            if (ModelState.IsValid)
            {
                _processor.BuyGame(request);
            }

            return View();
        }
    }
}
