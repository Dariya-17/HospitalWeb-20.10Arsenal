using Data_Hospital;
using Data_Hospital.Entities;
using Hospital_ManagerCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_ManagerCore.Services
{
    public class DoctorServices : IDoctorService
    {
        private readonly HospitalDbContext _context;
        public DoctorServices(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task AddDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            var doctor= await _context.Doctors.Include(d=>d.Patients).FirstOrDefaultAsync(d => d.Id == id);
            if (doctor != null)
            {
                          
               _context.DoctorPatients.RemoveRange(doctor.Patients);
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Doctor>> GetAllDoctorsAync()
        {
           return await _context.Doctors
                .Include(d => d.Hospital).ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors
                .FindAsync(id);
        }

        public async Task<Doctor?> GetDoctorDetailsAsync(int id)
        {
           return await _context.Doctors
                .Include(d => d.Hospital)
                .Include(d => d.Patients)
                .ThenInclude(dp => dp.Patient)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
           await _context.SaveChangesAsync();
        }
    }
}
