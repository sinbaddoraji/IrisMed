﻿using IrisMed.Data;
using IrisMed.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return View(_context.Queries.ToList());
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
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: QueriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QueriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QueriesController/Delete/5
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
