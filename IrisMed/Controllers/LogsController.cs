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
    public class LogsController : Controller
    {
        private readonly LogsContext _context;
        private readonly UserManager<IrisUser> _userManager;

        public LogsController(LogsContext context, UserManager<IrisUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Logs
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null|| user != null && user.StaffType < 2)
            {
                return NotFound();
            }

            var log = new Logs()
            {
                Name = $"{user.FullName}",
                Action = "Accessed system logs",
                Timestamp = DateTime.Now.ToString()
            };
            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return View(await _context.Logs.ToListAsync());
        }


        // GET: Logs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null || user != null && user.StaffType < 2)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var logs = await _context.Logs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logs == null)
            {
                return NotFound();
            }

            return View(logs);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logs = await _context.Logs.FindAsync(id);
            _context.Logs.Remove(logs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogsExists(int id)
        {
            return _context.Logs.Any(e => e.Id == id);
        }
    }
}
