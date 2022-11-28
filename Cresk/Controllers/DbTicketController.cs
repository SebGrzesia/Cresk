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
using Cresk.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Cresk.Controllers
{
    public class DbTicketController : Controller
    {
        private readonly CreskContext _context;

        public DbTicketController(CreskContext context)
        {
            _context = context;
        }

        // GET: DbTicket
        public async Task<IActionResult> Index(string searchString, TicketStatus? ticketStatus, TicketPriority? ticketPriority)
        {
            var ticket = _context.DbTicket.AsQueryable();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                ticket = ticket.Where(s => s.Title.Contains(searchString));
            }

            if (ticketStatus.HasValue)
            {
                ticket = ticket.Where(t => t.Status == ticketStatus);
            }

            if (ticketPriority.HasValue)
            {
                ticket = ticket.Where(j => j.Priority == ticketPriority);
            }
            ticket = ticket.OrderByDescending(t => t.CreatedDate);
            var ticketsFromDatabase = await ticket.ToListAsync();

            var ticketListViewModel = ticketsFromDatabase.Select(ticketFromDatabase => new IndexDbTicketViewModel()
            {
                Description = ticketFromDatabase.Description,
                ModifyDate = ticketFromDatabase.ModifyData,
                EmailAddress = ticketFromDatabase.Email,
                TicketPriority = ticketFromDatabase.Priority,
                TicketStatus = ticketFromDatabase.Status,
                Id = ticketFromDatabase.Id,
                Title = ticketFromDatabase.Title,
                TicketDisplayNumber = ticketFromDatabase.TicketDisplayNumber
            });

            var indexViewModel = new TicketIndexViewModel();
            indexViewModel.IndexDbTicketViewModels = ticketListViewModel.ToList();
            indexViewModel.SearchString = searchString;
            indexViewModel.TicketStatus = ticketStatus;

            return View(indexViewModel);
        }

        // GET: DbTicket/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DbTicket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDbTicketViewModel vm)
        {


            DbTicket dbTicket = new DbTicket();
            dbTicket.Title = vm.Title;
            dbTicket.Description = vm.Description;
            dbTicket.Priority = vm.Priority;
            dbTicket.Email = vm.Email;
            dbTicket.CreatedDate = DateTime.Now;
            dbTicket.ModifyData = DateTime.Now;
            dbTicket.Status = TicketStatus.New;           

            //_context.DbTag.FirstOrDefault(m => m.Id == dbTicket.TagId);

            if (ModelState.IsValid)
            {
                _context.Add(dbTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dbTicket);
        }

        // GET: DbTicket/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DbTicket == null)
            {
                return NotFound();
            }

            var dbTicket = await _context.DbTicket.FindAsync(id);
            if (dbTicket == null)
            {
                return NotFound();
            }

            EditDbTicketViewModel vm = new EditDbTicketViewModel();
            vm.Title = dbTicket.Title;
            vm.Status = dbTicket.Status;
            vm.Priority = dbTicket.Priority;
            vm.Email = dbTicket.Email;
            vm.Description = dbTicket.Description;
            vm.CreateDate = dbTicket.CreatedDate;
            vm.ModifyDate = dbTicket.ModifyData;
            return View(vm);
        }

        // POST: DbTicket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDbTicketViewModel vm)
        {
            var dbTicket = await _context.DbTicket.FindAsync(vm.Id);
            if(dbTicket == null)
            {
                return View(vm);
            }

            dbTicket.Status = vm.Status;
            dbTicket.Priority = vm.Priority;
            dbTicket.Title = vm.Title;
            dbTicket.Email = vm.Email;
            dbTicket.Description = vm.Description;
            dbTicket.ModifyData = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: DbTicket/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DbTicket == null)
            {
                return NotFound();
            }

            var dbTicket = await _context.DbTicket
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbTicket == null)
            {
                return NotFound();
            }

            return View(dbTicket);
        }

        // POST: DbTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DbTicket == null)
            {
                return Problem("Entity set 'CreskContext.DbTicket'  is null.");
            }
            var dbTicket = await _context.DbTicket.FindAsync(id);
            if (dbTicket != null)
            {
                _context.DbTicket.Remove(dbTicket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DbTicketExists(string id)
        {
          return _context.DbTicket.Any(e => e.Id == id);
        }
    }
}
