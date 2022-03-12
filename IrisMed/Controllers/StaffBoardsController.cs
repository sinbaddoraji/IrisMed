#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IrisMed.Data;
using IrisMed.Models;
using Microsoft.AspNetCore.Identity;
using IrisMed.Areas.Identity.Data;

namespace IrisMed.Controllers
{
    public class StaffBoardsController : Controller
    {
        private readonly StaffBoardContext _context;
        private readonly UserManager<IrisUser> _userManager;

        public StaffBoardsController(StaffBoardContext context, UserManager<IrisUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: StaffBoards
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null || user.StaffType == 0 || user.StaffType == null)
            {
                return NotFound();
            }

            return View(await _context.StaffBoard.ToListAsync());
        }

        // GET: StaffBoards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null || user.StaffType == 0 || user.StaffType == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var staffBoard = await _context.StaffBoard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffBoard == null)
            {
                return NotFound();
            }

            return View(staffBoard);
        }

        // GET: StaffBoards/Create
        public async Task<IActionResult> CreateAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);


            if (user == null || user.StaffType == 0 || user.StaffType == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: StaffBoards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StaffName,StaffMessage")] StaffBoard staffBoard)
        {
            if (ModelState.IsValid)
            {
                staffBoard.StaffName = staffBoard.StaffName.Split('@')[0];
                _context.Add(staffBoard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffBoard);
        }

        // GET: StaffBoards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            


            if (user == null || user.StaffType == 0 || user.StaffType == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var staffBoard = await _context.StaffBoard.FindAsync(id);
            if (staffBoard == null || staffBoard.StaffName != user.UserName.Split('@')[0])
            {
                return NotFound();
            }
            return View(staffBoard);
        }

        // POST: StaffBoards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StaffName,StaffMessage")] StaffBoard staffBoard)
        {

            if (id != staffBoard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffBoard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffBoardExists(staffBoard.Id))
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
            return View(staffBoard);
        }

        // GET: StaffBoards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null || user.StaffType == 0 || user.StaffType == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var staffBoard = await _context.StaffBoard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffBoard == null || staffBoard.StaffName != user.UserName.Split('@')[0])
            {
                return NotFound();
            }

            return View(staffBoard);
        }

        // POST: StaffBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffBoard = await _context.StaffBoard.FindAsync(id);
            _context.StaffBoard.Remove(staffBoard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffBoardExists(int id)
        {
            return _context.StaffBoard.Any(e => e.Id == id);
        }
    }
}
