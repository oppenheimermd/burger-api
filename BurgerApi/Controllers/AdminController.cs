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
using BurgerApi.Models.Helpers;
using BurgerApi.Services;
using Microsoft.AspNetCore.Http;

namespace BurgerApi.Controllers
{
    public class AdminController : Controller
    {
        private readonly IBurgerService _burgerService;
        private readonly SiteConfig _siteSettings;



        public AdminController(IBurgerService burgerService, SiteConfig siteSettings)
        {
            _burgerService = burgerService;
            _siteSettings = siteSettings;
        }

        //  Burgers

        public async Task<IActionResult> Index(int? page)
        {
            var burgers = _burgerService.GetBurgersPageable(page);
            return View(burgers);
        }

        // GET: Burgers/Create
        public async Task<IActionResult> CreateBurger()
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
        public async Task<IActionResult> CreateBurger([Bind("Id,Description,InstagramUserId,InstagramSourceUrl,CuisineId,BurgerBaseId,BurgerImage,Verified")] Burger burger)
        {
            var cuisineddList = await _burgerService.GetCuisineDropListAsync();
            var burgerbaseList = await _burgerService.GetBurgerBaseDropListAsync();

            var burgerExist = await _burgerService.BurgerExistAsync(burger.InstagramSourceUrl);
            if (burgerExist)
            {
                ModelState.AddModelError("", "Burger with same link already exist!");
                ViewData["BurgerBase"] = new SelectList(burgerbaseList, "Id", "BaseName");
                ViewData["Cuisine"] = new SelectList(cuisineddList, "Id", "CuisineTitle");
                return View(burger);
            }

            //  Picture required
            if (HttpContext.Request.Form.Files.Count != 0)
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

        // GET: Admin/BurgerDetails/5
        public async Task<IActionResult> BurgerDetails(int? id)
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

        // GET: Admin/Edit/5
        public async Task<IActionResult> EditBurger(int? id)
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

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBurger(int id, [Bind("Id,Description,InstagramUserId,InstagramSourceUrl,CuisineId,BurgerBaseId,BurgerImageId,Verified")] Burger burger)
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

        // GET: Admin/Delete/5
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

        // POST: Admin/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var burger = await _context.Burgers.FindAsync(id);
            _context.Burgers.Remove(burger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/


        //  BurgerBases

        // GET: Admin/BurgerBases
        public async Task<IActionResult> BurgerBaseAll()
        {
            return View(await _burgerService.GetBurgersBaseAsync(int.Parse(_siteSettings.ItemsPerPage), 0));
        }

        // GET: Admin/Create
        public IActionResult CreateBurgerBase()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBurgerBase([Bind("Id,BaseName,BaseNameCode")] BurgerBase burgerBase)
        {
            if (ModelState.IsValid)
            {
                await _burgerService.SaveBurgerBaseAsync(burgerBase);
                return RedirectToAction(nameof(Index));
            }
            return View(burgerBase);
        }

        // GET: BurgerBases/Edit/5
        public async Task<IActionResult> EditBurgerBase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burgerBase = await _burgerService.GetBurgerBaseAsync(id);
            if (burgerBase == null)
            {
                return NotFound();
            }
            return View(burgerBase);
        }

        // POST: BurgerBases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBurgerBase(int id, [Bind("Id,BaseName,BaseNameCode")] BurgerBase burgerBase)
        {
            if (id != burgerBase.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(burgerBase);
            var editSuccess = await _burgerService.UpdateBurgerBaseAsync(burgerBase);
            if (editSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        // GET: Admin/BurgerBases/Details/5
        public async Task<IActionResult> BurgerBaseDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burgerBase = await _burgerService.GetBurgerBaseAsync(id);
            if (burgerBase == null)
            {
                return NotFound();
            }

            return View(burgerBase);
        }

        // GET: BurgerBases/Delete/5
        /*public async Task<IActionResult> DeleteBurgerBase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burgerBase = await _context.BurgerBases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (burgerBase == null)
            {
                return NotFound();
            }

            return View(burgerBase);
        }

        // POST: BurgerBases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBurgerBaseConfirmed(int id)
        {
            var burgerBase = await _context.BurgerBases.FindAsync(id);
            _context.BurgerBases.Remove(burgerBase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        //  Cuisine

        // GET: Admin/CuisinesAlll
        public async Task<IActionResult> CuisinesAll()
        {
            return View(await _burgerService.GetCuisineAsync(12, 0));
        }

        // GET: Admin/CreateCuisine
        public IActionResult CreateCuisine()
        {
            return View();
        }

        // POST: Admin/CreateCuisine
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCuisine([Bind("Id,CuisineCode,CuisineTitle")] Cuisine cuisine)
        {
            if (ModelState.IsValid)
            {
                await _burgerService.SaveCuisineAsync(cuisine);
                return RedirectToAction(nameof(Index));
            }
            return View(cuisine);
        }

        // GET: Admin/EditCuisine/5
        public async Task<IActionResult> EditCuisine(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuisine = await _burgerService.GetCuisineAsync(id);
            if (cuisine == null)
            {
                return NotFound();
            }
            return View(cuisine);
        }

        // POST: Admin/EditCuisine/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCuisine(int id, [Bind("Id,CuisineCode,CuisineTitle")] Cuisine cuisine)
        {
            if (id != cuisine.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(cuisine);
            var editSuccess = await _burgerService.UpdateCuisineAsync(cuisine);
            if (editSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        // GET: Cuisines/Details/5
        public async Task<IActionResult> CuisineDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuisine = await _burgerService.GetCuisineAsync(id);

            if (cuisine == null)
            {
                return NotFound();
            }

            return View(cuisine);
        }


        // GET: Cuisines/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuisine = await _context.Cuisines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuisine == null)
            {
                return NotFound();
            }

            return View(cuisine);
        }*/

        // POST: Cuisines/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuisine = await _context.Cuisines.FindAsync(id);
            _context.Cuisines.Remove(cuisine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/
    }
}