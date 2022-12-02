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
using Microsoft.AspNetCore.Authorization;
using Cresk.ViewModels.Category;

namespace Cresk.Controllers
{
    [Authorize]
    public class TicketCategoryController : Controller
    {
        private readonly CreskContext _context;

        public TicketCategoryController(CreskContext context)
        {
            _context = context;
        }

        // GET: DbTag
        public async Task<IActionResult> Index(string searchString)
        {
            var category = _context.TicketCategories.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                category = category.Where(t => t.Name!.Contains(searchString));
            }
            var categoriesFromDatabase = await category.ToListAsync();
            var categoriesListViewModel = categoriesFromDatabase.Select(categoryFromDatabase => new IndexCategoryViewModel()
            {
                Id = categoryFromDatabase.Id,
                Name = categoryFromDatabase.Name,
                Description = categoryFromDatabase.Description
            });

            return View(categoriesListViewModel);
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
        public async Task<IActionResult> Create(CreateCategoryViewModel vm)
        {
            TicketCategory category = new TicketCategory();
            category.Name = vm.Name;
            category.Description = vm.Description;

            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: DbTag/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TicketCategories == null)
            {
                return NotFound();
            }

            var category = await _context.TicketCategories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            EditCategoryViewModel vm = new EditCategoryViewModel();
            vm.Name = category.Name;
            vm.Description = category.Description;
            return View(vm);
        }

        // POST: DbTag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoryViewModel vm)
        {
            var category = await _context.TicketCategories.FindAsync(vm.Id);
            if (category == null)
            {
                return View(vm);
            }
            category.Name = vm.Name;
            category.Description = vm.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: DbTag/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TicketCategories == null)
            {
                return NotFound();
            }

            var category = await _context.TicketCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: DbTag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TicketCategories == null)
            {
                return Problem("Entity set 'CreskContext.Category'  is null.");
            }
            var category = await _context.TicketCategories.FindAsync(id);
            if (category != null)
            {
                _context.TicketCategories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
