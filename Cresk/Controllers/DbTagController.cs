using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cresk.Data;
using Cresk.Models;
using Cresk.ViewModels;
using System.Net.Sockets;

namespace Cresk.Controllers
{
    public class DbTagController : Controller
    {
        private readonly CreskContext _context;

        public DbTagController(CreskContext context)
        {
            _context = context;
        }

        // GET: DbTag
        public async Task<IActionResult> Index(string searchString)
        {
            var tag = _context.DbTag.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                tag = tag.Where(t => t.Name!.Contains(searchString));
            }
            var tagFromDatabase = await tag.ToListAsync();
            var tagsListViewModel = tagFromDatabase.Select(tagFromDatabase => new IndexDbTagViewModel()
            {
                Name = tagFromDatabase.Name,
                Description = tagFromDatabase.Description
            });

            return View(tagsListViewModel);
        }

        // GET: DbTag/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DbTag == null)
            {
                return NotFound();
            }

            var dbTag = await _context.DbTag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbTag == null)
            {
                return NotFound();
            }

            return View(dbTag);
        }

        // GET: DbTag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DbTag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] DbTag dbTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dbTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dbTag);
        }

        // GET: DbTag/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DbTag == null)
            {
                return NotFound();
            }

            var dbTag = await _context.DbTag.FindAsync(id);
            if (dbTag == null)
            {
                return NotFound();
            }
            return View(dbTag);
        }

        // POST: DbTag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Description")] DbTag dbTag)
        {
            if (id != dbTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DbTagExists(dbTag.Id))
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
            return View(dbTag);
        }

        // GET: DbTag/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DbTag == null)
            {
                return NotFound();
            }

            var dbTag = await _context.DbTag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbTag == null)
            {
                return NotFound();
            }

            return View(dbTag);
        }

        // POST: DbTag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DbTag == null)
            {
                return Problem("Entity set 'CreskContext.DbTag'  is null.");
            }
            var dbTag = await _context.DbTag.FindAsync(id);
            if (dbTag != null)
            {
                _context.DbTag.Remove(dbTag);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DbTagExists(string id)
        {
          return _context.DbTag.Any(e => e.Id == id);
        }
    }
}
