using Data_Hospital;
using Data_Hospital.Entities;
using Hospital_Library.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Library.Services
{
    public class HospitalServices:IHospitalService
    {
        private readonly HospitalDbContext context_new;
        public HospitalServices(HospitalDbContext context)
        {
            context_new=context;
        }
        public async Task<List<Hospital>> GetAllHospitals()
        {
            return await
                context_new.Hospitals
                .Where(h=>!h.IsDeleted)
                .ToListAsync();
        }
        public async  Task<Hospital?> GetHospitalById(int id)
        {
          return await context_new.Hospitals
                .Where(h => !h.IsDeleted && h.Id == id)
                .FirstOrDefaultAsync();
        }
 
        public async Task AddHospitalAsync(Hospital hospital)
        {
            context_new.Hospitals.AddAsync(hospital);
            await context_new.SaveChangesAsync();
        }

        public async Task UpdateHospitalAsync(Hospital hospital)
        {
            //context_new.Hospitals.Update(hospital);
            await context_new.SaveChangesAsync();
        }

        public async Task DeleteHospitalAsync(int id)
        {
           var hospital = context_new.Hospitals.Find(id);
            if (hospital != null)
            {
                hospital.IsDeleted = true;
               await context_new.SaveChangesAsync();
            }         
        }
    }
}
