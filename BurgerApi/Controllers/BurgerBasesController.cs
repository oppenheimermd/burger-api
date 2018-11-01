using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerApi.Data;
using BurgerApi.Models;

namespace BurgerApi.Controllers
{
    public class BurgerBasesController : Controller
    {
        private readonly BurgerContext _context;

        public BurgerBasesController(BurgerContext context)
        {
            _context = context;
        }

        // GET: BurgerBases
        public async Task<IActionResult> Index()
        {
            return View(await _context.BurgerBases.ToListAsync());
        }

        // GET: BurgerBases/Details/5
        public async Task<IActionResult> Details(int? id)
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
                _context.Add(burgerBase);
                await _context.SaveChangesAsync();
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

            var burgerBase = await _context.BurgerBases.FindAsync(id);
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(burgerBase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BurgerBaseExists(burgerBase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(burgerBase);
        }

        // GET: BurgerBases/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
        }

        private bool BurgerBaseExists(int id)
        {
            return _context.BurgerBases.Any(e => e.Id == id);
        }
    }
}
