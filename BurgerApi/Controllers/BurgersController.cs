using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerApi.Data;
using BurgerApi.Models;
using BurgerApi.Services;
using Microsoft.AspNetCore.Http;

namespace BurgerApi.Controllers
{
    public class BurgersController : Controller
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

        // GET: Burgers
        public async Task<IActionResult> Index()
        {
            return View(await _burgerService.GetBurgersAsync(int.Parse(_siteSettings.ItemsPerPage), 0));
        }

        // GET: Burgers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burger = await _burgerService.GetBurgerAsync(id);
            if (burger == null)
            {
                return NotFound();
            }

            return View(burger);
        }

        // GET: Burgers/Create
        public async Task<IActionResult> Create()
        {
            var cuisineddList = await _burgerService.GetCuisineDropListAsync();
            var burgerbaseList = await _burgerService.GetBurgerBaseDropListAsync();

            ViewData["BurgerBase"] = new SelectList(burgerbaseList, "Id", "BaseName");
            ViewData["Cuisine"] = new SelectList(cuisineddList, "Id", "CuisineTitle");
            return View();
        }

        // POST: Burgers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,InstagramUserId,InstagramSourceUrl,CuisineId,BurgerBaseId,BurgerImage")] Burger burger)
        {
            var cuisineddList = await _burgerService.GetCuisineDropListAsync();
            var burgerbaseList = await _burgerService.GetBurgerBaseDropListAsync();

            //  Picture required
            if (HttpContext.Request.Form.Files.Count != 0 )
            {

                if (ModelState.IsValid)
                {
                    var burgerPhoto = HttpContext.Request.Form.Files[0];
                    //  yes
                    if (burgerPhoto.Length > 0)
                    {
                        var newBurgerPhoto = await _burgerService.SaveBurgerPhotoAsync(burgerPhoto);
                        await _burgerService.SaveBurgerImage(newBurgerPhoto);
                        burger.BurgerImageId = newBurgerPhoto.Id;
                        await _burgerService.SaveBurgerAsync(burger);
                        

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Image missing");
                        ViewData["BurgerBase"] = new SelectList(burgerbaseList, "Id", "BaseName");
                        ViewData["Cuisine"] = new SelectList(cuisineddList, "Id", "CuisineTitle");
                        return View(burger);
                    }

                }
                else
                {

                    ViewData["BurgerBase"] = new SelectList(burgerbaseList, "Id", "BaseName");
                    ViewData["Cuisine"] = new SelectList(cuisineddList, "Id", "CuisineTitle");
                    return View(burger);
                }

            }
            else
            {
                ModelState.AddModelError("", "Image required");

                ViewData["BurgerBase"] = new SelectList(burgerbaseList, "Id", "BaseName");
                ViewData["Cuisine"] = new SelectList(cuisineddList, "Id", "CuisineTitle");
                return View(burger);
            }
        }

        // GET: Burgers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burger = await _burgerService.GetBurgerAsync(id);
            if (burger == null)
            {
                return NotFound();
            }

            var cuisineddList = await _burgerService.GetCuisineDropListAsync();
            var burgerbaseList = await _burgerService.GetBurgerBaseDropListAsync();

            ViewData["BurgerBase"] = new SelectList(burgerbaseList, "Id", "BaseName");
            ViewData["Cuisine"] = new SelectList(cuisineddList, "Id", "CuisineTitle");
            return View(burger);
        }

        // POST: Burgers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,InstagramUserId,InstagramSourceUrl,CuisineId,BurgerBaseId")] Burger burger)
        {
            if (id != burger.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(burger);
            var editSuccess = await _burgerService.UpdateBurgerAsync(burger);
            if (editSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        // GET: Burgers/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burger = await _context.Burgers
                .Include(b => b.BurgerBase)
                .Include(b => b.Cuisine)
                .Include(b => b.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (burger == null)
            {
                return NotFound();
            }

            return View(burger);
        }*/

        // POST: Burgers/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var burger = await _context.Burgers.FindAsync(id);
            _context.Burgers.Remove(burger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

 

    }
}
