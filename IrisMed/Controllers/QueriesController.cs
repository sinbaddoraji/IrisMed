using IrisMed.Areas.Identity.Data;
using IrisMed.Data;
using IrisMed.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IrisMed.Controllers
{
    public class QueriesController : Controller
    {

        private ContactUsContext _context;
        private readonly UserManager<IrisUser> _userManager;
        private readonly LogsContext _logsContext;

        public QueriesController(ContactUsContext context, UserManager<IrisUser> userManager
            , LogsContext logsContext)
        {
            _context = context;
            _userManager = userManager;
            _logsContext = logsContext;
        }


        // GET: QueriesController
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0 || user.StaffType == null)
            {
                return RedirectToAction(nameof(Create));
            }
            return View(_context.Queries.Select(x => ToModel(x)).ToList());
        }

        private static ContactUs ToModel(PatientQueries patientQueries)
        {
            return new ContactUs()
            {
                Id = patientQueries.Id,
                Name = patientQueries.Name,
                Content = patientQueries.Content,
                Email = patientQueries.Email
            };    
        }

        // GET: QueriesController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0 || user.StaffType == null)
            {
                return RedirectToAction(nameof(Create));
            }

            return View();
        }

        // GET: QueriesController/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // POST: QueriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]PatientQueries collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(collection);
                    _context.SaveChanges();
                    _context.Queries.Add(collection);


                    var log = new Logs()
                    {
                        Name = $"{collection.Name}",
                        Action = "sumbitted a query",
                        Timestamp = DateTime.Now.ToString()
                    };

                    _logsContext.Add(log);
                    _logsContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && user.StaffType == 0 || user.StaffType == null)
            {
                return RedirectToAction(nameof(Create));
            }

            return View();
        }

        // POST: QueriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var query = _context.Queries.Where(x => x.Id == id).First();
                    if(query != null)
                    {
                        _context.Queries.Remove(query);
                        _context.Remove(query);
                        _context.SaveChanges();
                    }
                }
                    

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
