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
    public class PatientServices : IPatientServices
    {
        private readonly HospitalDbContext _context;
        public PatientServices(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
           var patient = await _context.Patients.Include(d => d.Doctors).FirstOrDefaultAsync(d => d.Id == id);
            if (patient != null)
            {
                _context.DoctorPatients.RemoveRange(patient.Doctors);
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
          return await _context.Patients.Include(d=>d.Doctors)
                .ThenInclude(dp => dp.Doctor).ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _context.Patients
                .FindAsync(id);
        }

        public async Task UpdatePatientAsync(Patient patient,List<int>doctorsId)
        {
            patient.Doctors.Clear();
            foreach(var doctorId in doctorsId.Distinct())
            {
                patient.Doctors.Add(new DoctorPatient { DoctorId = doctorId, PatientId = patient.Id });
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            throw new NotImplementedException();
        }
        public async Task<Patient?> GetPatientWithDoctorAsync(int id)
        {
            return await _context.Patients.Include(d => d.Doctors)
                .ThenInclude(dp => dp.Doctor)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
