using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurgerApi.Models;
using BurgerApi.Services;

namespace BurgerApi.Controllers
{
    public class CuisinesController : Controller
    {
        private readonly IBurgerService _burgerService;

        public CuisinesController(IBurgerService burgerService)
        {
            _burgerService = burgerService;
        }

        // GET: Cuisines
        public async Task<IActionResult> Index()
        {
            return View(await _burgerService.GetCuisineAsync(12, 0));
        }

        // GET: Cuisines/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Cuisines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cuisines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CuisineCode,CuisineTitle")] Cuisine cuisine)
        {
            if (ModelState.IsValid)
            {
                await _burgerService.SaveCuisineAsync(cuisine);
                return RedirectToAction(nameof(Index));
            }
            return View(cuisine);
        }

        // GET: Cuisines/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Cuisines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CuisineCode,CuisineTitle")] Cuisine cuisine)
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
