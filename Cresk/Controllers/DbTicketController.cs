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
using Cresk.ViewModels.Tickets;
using Cresk.ViewModels.Chat;
using Microsoft.AspNetCore.Identity;

namespace Cresk.Controllers
{
    public class DbTicketController : Controller
    {
        private readonly CreskContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DbTicketController(CreskContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: DbTicket
        public async Task<IActionResult> Index(string searchString, TicketStatus? ticketStatus, TicketPriority? ticketPriority, string categoryId)
        {
            var ticket = _context.DbTicket.Include(i => i.Category).AsQueryable();
            
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

            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                ticket = ticket.Where(k => k.CategoryId == categoryId);
            }

            ticket = ticket.OrderByDescending(t => t.CreatedDate);
            var ticketsFromDatabase = await ticket.ToListAsync();
            var tags = await _context.TicketCategories.Select(categoryFromDatabase => new SelectListItem()
            {
                Value = categoryFromDatabase.Id,
                Text = categoryFromDatabase.Name
            }).ToListAsync();


            var ticketListViewModel = ticketsFromDatabase.Select(ticketFromDatabase => new IndexDbTicketViewModel()
            {
                CategoryName = ticketFromDatabase.Category!=null?ticketFromDatabase.Category.Name:"----", 
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
            indexViewModel.CategoryList= tags;

            return View(indexViewModel);
        }

        // GET: DbTicket/Create
        public async Task<IActionResult> Create()
        {
            var tags = await _context.TicketCategories.Select(categoryFromDatabase => new SelectListItem()
            {
                Value = categoryFromDatabase.Id,
                Text = categoryFromDatabase.Name
            }).ToListAsync();
            CreateDbTicketViewModel vm = new CreateDbTicketViewModel();
            vm.CategoryList = tags;
            return View(vm);
        }

        // POST: DbTicket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDbTicketViewModel vm)
        {

            DbTicket dbTicket = new DbTicket();
            if (!string.IsNullOrWhiteSpace(vm.CategoryId))
            {
                dbTicket.CategoryId = vm.CategoryId;
            }
            dbTicket.Title = vm.Title;
            dbTicket.Description = vm.Description;
            dbTicket.Priority = vm.Priority;
            dbTicket.Email = User.Identity.Name;
            dbTicket.CreatedDate = DateTime.Now;
            dbTicket.ModifyData = DateTime.Now;
            dbTicket.Status = TicketStatus.New;           

            _context.Add(dbTicket);
            await _context.SaveChangesAsync();

            var chat = new Chat();
            chat.DbTicketId = dbTicket.Id;
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
            var tags = await _context.TicketCategories.Select(categoryFromDatabase => new SelectListItem()
            {
                Value = categoryFromDatabase.Id,
                Text = categoryFromDatabase.Name
            }).ToListAsync();

            var chat = await _context.Chats.FirstOrDefaultAsync(m => m.DbTicketId == dbTicket.Id);
            var chatMessages = await _context.ChatMessages
                .Where(t => t.ChatId == chat.Id)
                .OrderByDescending(t => t.CreateTime)
                .Select(t => new ChatMessageViewModel()
                {
                    CreateTime = t.CreateTime,
                    Message = t.Message,
                    Username = t.Username
                })
                .ToListAsync();

            EditDbTicketViewModel vm = new EditDbTicketViewModel();
            vm.CategoryList = tags;
            vm.Title = dbTicket.Title;
            vm.Status = dbTicket.Status;
            vm.Priority = dbTicket.Priority;
            vm.Email = dbTicket.Email;
            vm.Description = dbTicket.Description;
            vm.CreateDate = dbTicket.CreatedDate;
            vm.ModifyDate = dbTicket.ModifyData;
            vm.ChatMessageViewModels = chatMessages;
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
            if (!string.IsNullOrWhiteSpace(vm.CategoryId))
            {
                dbTicket.CategoryId = vm.CategoryId;
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

        [HttpPost]
        public async Task<IActionResult> AddMessage(string message, string dbTicketId)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(m => m.DbTicketId == dbTicketId);
            var userId = _userManager.GetUserId(User);
            var chatMessage = new ChatMessage()
            {
                ChatId = chat.Id,
                CreateTime = DateTime.Now,
                Message = message,
                UserId = userId,
                Username = User.Identity.Name
            };
            await _context.ChatMessages.AddAsync(chatMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new {id = dbTicketId});
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
    }
}
