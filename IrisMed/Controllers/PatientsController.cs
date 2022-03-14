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
using IrisMed.Areas.Identity.Data;

namespace IrisMed.Controllers
{
    public class PatientsController : Controller
    {
        private readonly AppointmentsContext _context;

        public PatientsController(AppointmentsContext context)
        {
            _context = context;
        }


    }
}
