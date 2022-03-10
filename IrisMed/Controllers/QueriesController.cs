using IrisMed.Data;
using IrisMed.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IrisMed.Controllers
{
    public class QueriesController : Controller
    {

        private ContactUsContext _context;

        public QueriesController(ContactUsContext context)
        {
            _context = context;
        }


        // GET: QueriesController
        public ActionResult Index()
        {
            return View(_context.Queries.Select(x => ToModel(x)).ToList());
        }

        private static ContactUsModel ToModel(PatientQueries patientQueries)
        {
            return new ContactUsModel()
            {
                Id = patientQueries.Id,
                Name = patientQueries.Name,
                Content = patientQueries.Content,
                Email = patientQueries.Email
            };    
        }

        // GET: QueriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QueriesController/Create
        public ActionResult Create()
        {
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

                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
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
