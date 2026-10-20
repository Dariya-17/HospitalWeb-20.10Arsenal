using Data_Hospital;
using Data_Hospital.Entities;
using HospitalWeb.ViewModels.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalWeb.Controllers
{
    public class PatientController : Controller
    {
        private readonly HospitalDbContext context;
        public PatientController(HospitalDbContext context)//tozi obekt se podava AVTOMATICHNO
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var patients = await context.Patients.Include(x => x.Doctors).ThenInclude(x => x.Doctor).ToListAsync();
            var model = new List<PatientIndexViewModel>();
            foreach (var item in patients)
            {
                model.Add(new PatientIndexViewModel
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    DoctorName = string.Join(", ", item.Doctors.Select(d => d.Doctor.FirstName + " " + d.Doctor.LastName))
                });
            }
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            Patient patient = await context.Patients
                .Include(p => p.Doctors)
                .ThenInclude(pd => pd.Doctor)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            var model = new PatientDetailsViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                DoctorName = string.Join(", ", patient.Doctors.Select(d => d.Doctor.FirstName + " " + d.Doctor.LastName))
            };
            return View(model);
        }
        private async Task LoadDoctors(List<int> selectedDoctors = null)
        {
            var doctors = await context.Doctors
                .Select(d => new
                {
                    d.Id,
                    FullName = d.FirstName + " " + d.LastName

                })
                .ToListAsync();
            ViewBag.Doctors = new MultiSelectList(
                doctors,
                "Id",
                "FullName",
                selectedDoctors
                );
        }
        public async Task<IActionResult> Create()
        {
            await LoadDoctors();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PatientCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                foreach (var doctorId in model.DoctorIds)//direktno 
                {
                    patient.Doctors.Add(new DoctorPatient
                    {
                        //avtomatichno pravi id-to na pacienta
                        DoctorId = doctorId
                    });
                }
                context.Patients.Add(patient);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadDoctors(model.DoctorIds);
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await context.Patients
                .Include(p => p.Doctors)
                .ThenInclude(dp => dp.Doctor)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            var model = new PatientEditViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                DoctorIds = patient.Doctors.Select(dp => dp.DoctorId).ToList()
            };
            await LoadDoctors(model.DoctorIds);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,PatientEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                var patient = await context.Patients
                .Include(p => p.Doctors)
                .ThenInclude(dp => dp.Doctor)
                .FirstOrDefaultAsync(p => p.Id == id);
                if (patient == null)
                {
                    return NotFound();
                }
                patient.FirstName=model.FirstName;
                patient.LastName = model.LastName;
                patient.PhoneNumber = model.PhoneNumber;
                patient.Email = model.Email;
                patient.Doctors.Clear();
                foreach (var item in model.DoctorIds)
                {
                    patient.Doctors.Add(new DoctorPatient
                    {
                        DoctorId = item,
                        PatientId = patient.Id
                    });
                }
                await context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index));
            }
            await LoadDoctors(model.DoctorIds);
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await context.Patients.FindAsync(id);
            if(patient == null)
            {
                return NotFound();
            }
            var model = new PatientDeleteViewModel
            {
                Id=patient.Id,
                FirstName=patient.FirstName,
                LastName=patient.LastName
            };
            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await context.Patients.FindAsync(id);
            if (patient != null)
            {
                context.Patients.Remove(patient);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
