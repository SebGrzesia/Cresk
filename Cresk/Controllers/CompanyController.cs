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
using Cresk.ViewModels.Company;

namespace Cresk.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CreskContext _context;

        public CompanyController(CreskContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var company = _context.Companies.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                company = company.Where(t => t.CompanyName!.Contains(searchString));
            }
            var companyFromDatabase = await company.ToListAsync();
            var companiesListViewModel = companyFromDatabase.Select(companyFromDatabase => new IndexCompanyViewModel()
            {
                Id = companyFromDatabase.Id,
                CompanyName = companyFromDatabase.CompanyName
            });

            return View(companiesListViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCompanyViewModel vm)
        {
            Company company = new Company();
            company.CompanyName = vm.CompanyName;

            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: DbTag/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            EditCompanyViewModel vm = new EditCompanyViewModel();
            vm.CompanyName = company.CompanyName;
            return View(vm);
        }

        // POST: DbTag/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCompanyViewModel vm)
        {
            var company = await _context.Companies.FindAsync(vm.Id);
            if (company == null)
            {
                return View(vm);
            }
            company.CompanyName = vm.CompanyName;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: DbTag/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: DbTag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Companies == null)
            {
                return Problem("Entity set 'CreskContext.Companies'  is null.");
            }
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
