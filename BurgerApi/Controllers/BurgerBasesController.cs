using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurgerApi.Models;
using BurgerApi.Services;

namespace BurgerApi.Controllers
{
    public class BurgerBasesController : Controller
    {
        private readonly IBurgerService _burgerService;
        private readonly SiteConfig _siteSettings;

        public BurgerBasesController(IBurgerService burgerService, SiteConfig siteSettings)
        {
            _burgerService = burgerService;
            _siteSettings = siteSettings;
        }

        // GET: BurgerBases
        public async Task<IActionResult> Index()
        {
            return View(await _burgerService.GetBurgersBaseAsync(int.Parse(_siteSettings.ItemsPerPage),0));
        }

        // GET: BurgerBases/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: BurgerBases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BurgerBases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BaseName,BaseNameCode")] BurgerBase burgerBase)
        {
            if (ModelState.IsValid)
            {
                await _burgerService.SaveBurgerBaseAsync(burgerBase);
                return RedirectToAction(nameof(Index));
            }
            return View(burgerBase);
        }

        // GET: BurgerBases/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,BaseName,BaseNameCode")] BurgerBase burgerBase)
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

        // GET: BurgerBases/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var burgerBase = await _context.BurgerBases.FindAsync(id);
            _context.BurgerBases.Remove(burgerBase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

    }
}
