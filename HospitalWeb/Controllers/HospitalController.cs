using Data_Hospital;
using Data_Hospital.Entities;
using HospitalWeb.Models;
using HospitalWeb.ViewModels.Hospital;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalWeb.Controllers
{
    public class HospitalController : Controller
    {
        private readonly HospitalDbContext context;
        public HospitalController(HospitalDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var hospitals = await context.Hospitals.ToListAsync();

            var model = new List<HospitalIndexViewModel>();

            foreach (var item in hospitals)
            {
                model.Add(new HospitalIndexViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    BedsCount = item.BedsCount
                });
            }
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var hospital = await context.Hospitals.FindAsync(id);

            if(hospital == null)
            {
                return NotFound();
            }

            var model = new HospitalDetailsViewModel
            {
                Id = hospital.Id,
                Name=hospital.Name,
                Address = hospital.Address,
                Email = hospital.Email,
                PhoneNumber=hospital.PhoneNumber,
                BedsCount=hospital.BedsCount,
                CreatedOn=hospital.CreatedOn
            };
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HospitalCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hospital = new Data_Hospital.Entities.Hospital
                {
                    Name = model.Name,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    BedsCount = model.BedsCount,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                };
                context.Hospitals.Add(hospital);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var hospital = await context.Hospitals.FindAsync(id);

            if (hospital == null)
            {
                return NotFound();
            }

            var model = new HospitalEditViewModel
            {
                Id = hospital.Id,
                Name = hospital.Name,
                Address = hospital.Address,
                Email = hospital.Email,
                PhoneNumber = hospital.PhoneNumber,
                BedsCount = hospital.BedsCount
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,HospitalEditViewModel model)
        {
            if (id!=model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var hospital = await context.Hospitals.FindAsync(id);

                if (hospital == null)
                {
                    return NotFound();
                }

                hospital.Name = model.Name;
                hospital.Address= model.Address;
                hospital.Email= model.Email;
                hospital.PhoneNumber = model.PhoneNumber;
                hospital.BedsCount = model.BedsCount;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            var model = new HospitalDeleteViewModel
            {
                Id = hospital.Id,
                Name = hospital.Name,
            };
            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospital = await context.Hospitals.FindAsync(id);
            if (hospital != null)
            {
                context.Hospitals.Remove(hospital);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    } 
}
