﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbFirstWebApp.Models;

namespace DbFirstWebApp.Controllers
{
    public class MembertblsController : Controller
    {
        private readonly BookrentalshopContext _context;

        public MembertblsController(BookrentalshopContext context)
        {
            _context = context;
        }

        // GET: Membertbls
        public async Task<IActionResult> Index()
        {
            return View(await _context.Membertbls.ToListAsync());
        }

        // GET: Membertbls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membertbl = await _context.Membertbls
                .FirstOrDefaultAsync(m => m.Idx == id);
            if (membertbl == null)
            {
                return NotFound();
            }

            return View(membertbl);
        }

        // GET: Membertbls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Membertbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idx,Names,Levels,Addr,Mobile,Email")] Membertbl membertbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membertbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membertbl);
        }

        // GET: Membertbls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membertbl = await _context.Membertbls.FindAsync(id);
            if (membertbl == null)
            {
                return NotFound();
            }
            return View(membertbl);
        }

        // POST: Membertbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idx,Names,Levels,Addr,Mobile,Email")] Membertbl membertbl)
        {
            if (id != membertbl.Idx)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membertbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembertblExists(membertbl.Idx))
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
            return View(membertbl);
        }

        // GET: Membertbls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membertbl = await _context.Membertbls
                .FirstOrDefaultAsync(m => m.Idx == id);
            if (membertbl == null)
            {
                return NotFound();
            }

            return View(membertbl);
        }

        // POST: Membertbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membertbl = await _context.Membertbls.FindAsync(id);
            if (membertbl != null)
            {
                _context.Membertbls.Remove(membertbl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembertblExists(int id)
        {
            return _context.Membertbls.Any(e => e.Idx == id);
        }
    }
}
