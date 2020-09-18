using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Core.Interface;
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
            return View();
        }
    }
}
