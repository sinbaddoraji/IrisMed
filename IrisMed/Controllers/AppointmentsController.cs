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
    public class AppointmentsController : Controller
    {
        private readonly AppointmentsContext _context;
        private readonly UserManager<IrisUser> _userManager;
        private static Random _random = new Random();
        private static string[] doctors = { "Dr Emeka", "Dr Lui", "Dr Chen" };
        private readonly LogsContext _logsContext;

        public AppointmentsController(AppointmentsContext context, UserManager<IrisUser> userManager
            , LogsContext logsContext)
        {
            _context = context;
            _userManager = userManager;
            _logsContext = logsContext;
        }


        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0)
            {
                return RedirectToAction(nameof(Create));
            }
            return View(await _context.Appointments.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0)
            {
                return RedirectToAction(nameof(Create));
            }
            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // GET: Appointments/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DoctorName,PatientName,PatientComplaints,AppointmentTime,AppointmentDate")] Appointment appointments)
        {
            if (ModelState.IsValid)
            {
                appointments.PatientName = appointments.PatientName.Split('@')[0];
                appointments.DoctorName = doctors[_random.Next(0,doctors.Length)];

                var date = DateTime.Parse(appointments.AppointmentDate);

                if (date < DateTime.Now)
                {
                    return View(appointments);
                }

                _context.Add(appointments);
                await _context.SaveChangesAsync();

                var log = new Logs()
                {
                    Name = $"{appointments.PatientName}",
                    Action = $"booked an appointment with {appointments.DoctorName}",
                    Timestamp = DateTime.Now.ToString()
                };
                await _logsContext.AddAsync(log);
                await _logsContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(appointments);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0)
            {
                return RedirectToAction(nameof(Create));
            }

            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments.FindAsync(id);
            if (appointments == null)
            {
                return NotFound();
            }
            return View(appointments);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DoctorName,PatientName,PatientComplaints,AppointmentTime,AppointmentDate")] Appointment appointments)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0)
            {
                return RedirectToAction(nameof(Create));
            }

            if (id != appointments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentsExists(appointments.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                var date = DateTime.Parse(appointments.AppointmentDate);

                if (date >= DateTime.Now)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(appointments);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null & user.StaffType == 0)
            {
                return RedirectToAction(nameof(Create));
            }

            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointments = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentsExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
