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

namespace IrisMed.Controllers
{
    public class CareersModelsController : Controller
    {
        private readonly CareersContext _context;

        public CareersModelsController(CareersContext context)
        {
            _context = context;
        }

        // GET: CareersModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.CareersModel.ToListAsync());
        }

        // GET: CareersModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careersModel = await _context.CareersModel
                .FirstOrDefaultAsync(m => m.Email == id);
            if (careersModel == null)
            {
                return NotFound();
            }

            return View(careersModel);
        }

        // GET: CareersModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CareersModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Resume,CoverLetter")] Career careersModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(careersModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(careersModel);
        }

        // GET: CareersModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careersModel = await _context.CareersModel.FindAsync(id);
            if (careersModel == null)
            {
                return NotFound();
            }
            return View(careersModel);
        }

        // POST: CareersModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Email,Resume,CoverLetter")] Career careersModel)
        {
            if (id != careersModel.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(careersModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CareersModelExists(careersModel.Email))
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
            return View(careersModel);
        }

        // GET: CareersModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careersModel = await _context.CareersModel
                .FirstOrDefaultAsync(m => m.Email == id);
            if (careersModel == null)
            {
                return NotFound();
            }

            return View(careersModel);
        }

        // POST: CareersModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var careersModel = await _context.CareersModel.FindAsync(id);
            _context.CareersModel.Remove(careersModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CareersModelExists(string id)
        {
            return _context.CareersModel.Any(e => e.Email == id);
        }
    }
}
