﻿using System;
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
    public class BurgersController : Controller
    {
        private readonly BurgerContext _context;

        public BurgersController(BurgerContext context)
        {
            _context = context;
        }

        // GET: Burgers
        public async Task<IActionResult> Index()
        {
            var burgerContext = _context.Burgers.Include(b => b.BurgerBase).Include(b => b.Cuisine).Include(b => b.Image);
            return View(await burgerContext.ToListAsync());
        }

        // GET: Burgers/Details/5
        public async Task<IActionResult> Details(int? id)
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
        }

        // GET: Burgers/Create
        public IActionResult Create()
        {
            ViewData["BurgerBaseId"] = new SelectList(_context.BurgerBases, "Id", "BaseName");
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "CuisineCode");
            ViewData["BurgerImageId"] = new SelectList(_context.Images, "Id", "FileName");
            return View();
        }

        // POST: Burgers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Verified,CuisineId,BurgerBaseId,BurgerImageId")] Burger burger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(burger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BurgerBaseId"] = new SelectList(_context.BurgerBases, "Id", "BaseName", burger.BurgerBaseId);
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "CuisineCode", burger.CuisineId);
            ViewData["BurgerImageId"] = new SelectList(_context.Images, "Id", "FileName", burger.BurgerImageId);
            return View(burger);
        }

        // GET: Burgers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burger = await _context.Burgers.FindAsync(id);
            if (burger == null)
            {
                return NotFound();
            }
            ViewData["BurgerBaseId"] = new SelectList(_context.BurgerBases, "Id", "BaseName", burger.BurgerBaseId);
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "CuisineCode", burger.CuisineId);
            ViewData["BurgerImageId"] = new SelectList(_context.Images, "Id", "FileName", burger.BurgerImageId);
            return View(burger);
        }

        // POST: Burgers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Verified,CuisineId,BurgerBaseId,BurgerImageId")] Burger burger)
        {
            if (id != burger.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(burger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BurgerExists(burger.Id))
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
            ViewData["BurgerBaseId"] = new SelectList(_context.BurgerBases, "Id", "BaseName", burger.BurgerBaseId);
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "CuisineCode", burger.CuisineId);
            ViewData["BurgerImageId"] = new SelectList(_context.Images, "Id", "FileName", burger.BurgerImageId);
            return View(burger);
        }

        // GET: Burgers/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
        }

        // POST: Burgers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var burger = await _context.Burgers.FindAsync(id);
            _context.Burgers.Remove(burger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BurgerExists(int id)
        {
            return _context.Burgers.Any(e => e.Id == id);
        }
    }
}
