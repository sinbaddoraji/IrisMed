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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IrisUser> _userManager;
        private static readonly Random _random = new();
        private static string[] doctors;

        public AppointmentsController(UserManager<IrisUser> userManager,ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            doctors = _context.Users.Where(x => x.StaffType == 1).Select(x => x.FullName).ToArray();
        }


        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0 && !_context.Appointments.Where(x=> x.PatientName == user.FullName).Any())
            {
                return RedirectToAction(nameof(Create));
            }
            if(user.StaffType == 0)
            {
                return RedirectToAction(nameof(Appointment));
            }
            return View(await _context.Appointments.ToListAsync());
        }

        // GET: Appointments
        public async Task<IActionResult> Appointment()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0 && !_context.Appointments.Where(x => x.PatientName == user.FullName).Any())
            {
                return RedirectToAction(nameof(Create));
            }

            return View(await _context.Appointments.Where(x => x.PatientName == user.FullName).ToListAsync());
        }

        // GET: Patients
        public async Task<IActionResult> Patient()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0)
            {
                return RedirectToAction(nameof(Create));
            }
            
            var patients = _context.Users.Where(x => x.StaffType == 0).ToList();
            string doctor = user.FullName;

            var fileterdPatients = new List<IrisUser>();
            foreach(var patient in patients)
            {
                if(_context.Appointments.Where(x => x.PatientName == patient.FullName && x.DoctorName.Equals(doctor)).Any())
                {
                    fileterdPatients.Add(patient);
                }
            }

            
            return View(fileterdPatients.Distinct());
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> DiagnoseAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0)
            {
                return RedirectToAction(nameof(Create));
            }

            var patients = _context.Users.Where(x => x.StaffType == 0).ToList();
            string doctor = user.FullName;

            var fileterdPatients = new List<IrisUser>();
            foreach (var patient in patients)
            {
                if (_context.Appointments.Where(x => x.PatientName == patient.FullName && x.DoctorName.Equals(doctor)).Any())
                {
                    fileterdPatients.Add(patient);
                }
            }

            var pt = fileterdPatients.First();
            if (pt == null)
            {
                return NotFound();
            }
            return View(pt);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Diagnose(string id, [Bind("FullName,Gender,DateOfBirth,Height,Weight,AssignedMedication,MedicalConditons, MedicalBill")] IrisUser patient)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            if (id == null)
            {
                return NotFound();
            }

            if (id != patient.FullName.Replace(" ", ""))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var p = _context.Users.Where(x => x.FullName == patient.FullName).First();
                    p.MedicalConditons = patient.MedicalConditons;
                    p.AssignedMedication = patient.MedicalConditons;


                    _context.Update(p);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.FullName, user))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Patient));
            }
            return View(patient);
        }


        private bool PatientExists(string id, IrisUser user)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var patients = _context.Users.Where(x => x.StaffType == 0).ToList();
            string doctor = user.FullName;

            var fileterdPatients = new List<IrisUser>();
            foreach (var patient in patients)
            {
                if (_context.Appointments.Where(x => x.PatientName == patient.FullName && x.DoctorName.Equals(doctor)).Any())
                {
                    fileterdPatients.Add(patient);
                }
            }
            return fileterdPatients.Any();
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
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                appointments.PatientName = user.FullName;
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
                await _context.AddAsync(log);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(appointments);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Pay(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType > 0)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType > 0)
            {
                return NotFound();
            }

            var appointment = await _context.FindAsync<Appointment>(id);

            if (ModelState.IsValid)
            {
                try
                {
                    appointment.MedicalBill = -1;
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentsExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return RedirectToAction(nameof(Appointment));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,DoctorName,PatientName,PatientComplaints,AppointmentTime,AppointmentDate,MedicalBill")] Appointment appointments)
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

                return RedirectToAction(nameof(Index));
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
