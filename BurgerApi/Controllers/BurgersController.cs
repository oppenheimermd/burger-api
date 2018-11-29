using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurgerApi.Data;
using BurgerApi.Models;
using BurgerApi.Services;


namespace BurgerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BurgersController : ControllerBase
    {
        private readonly IBurgerService _burgerService;
        private readonly SiteConfig _siteSettings;

        private readonly BurgerContext _context;

        public BurgersController(BurgerContext context, IBurgerService burgerService, SiteConfig siteSettings)
        {
            _context = context;
            _burgerService = burgerService;
            _siteSettings = siteSettings;
        }

    }
}
