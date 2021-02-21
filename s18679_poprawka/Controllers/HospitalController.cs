using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s18679_poprawka.Controllers.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace s18679_poprawka.Controllers
{
    public class HospitalController : ControllerBase
    {
        private readonly HospitalDbContext hospitalDbContext;

        public HospitalController(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetPrescription(int id)
        {
            if (hospitalDbContext.Prescription.Where(d => d.IdPrescription == id).Any())
            {
                var medicament = hospitalDbContext.Prescription_Medicament
                    .Include(d=>d.IdMedicament)
                    .Where(d => d.IdPrescription == id).Select(d=>d.IdMedicament)
                    .ToList();
                var prescription = hospitalDbContext.Medicament.SingleOrDefault(d => d.IdMedicament == id);
                return Ok(prescription);
            }
            return NotFound();
        }

        
    }
}