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
            if (!ModelState.IsValid)
            {
                return View();               
            }
            var result = _processor.BuyGame(request);
            if (result.StatusCode == GameBuyingResultCode.GameIsNotAvailable)
                return View
                    (
                    "Views/Home/GameIsNotAvaliable.cshtml",
                        new ErrorModel()
                        {
                            Message = "Nie ma juz gry w magazynie"
                        }
                    );

            return View("Views/Home/Success.cshtml");
        }

    }
}
