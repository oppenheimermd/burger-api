using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BurgerApi.Controllers
{
    public class AdminController : Controller
    {
        private readonly IBurgerService _burgerService;

        public AdminController(IBurgerService burgerService)
        {
            _burgerService = burgerService;
        }

        public async Task<IActionResult> Index()
        {
            //var burgers = await _burgerService.GetBurgersAsync(12, 0);
            //return View(burgers);
            return View();
        }
    }
}