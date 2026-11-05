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
        public async Task<List<Hospital>> GetAllHospitals(string ?search,int? minBeds,string ?sortOrder)
        {
            var query = context_new.Hospitals
                 .Where(h => !h.IsDeleted);
                
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(h => h.Name.Contains(search));
            }
            if(minBeds.HasValue)
            {
                query = query.Where(h => h.BedsCount >= minBeds.Value);
            }
         switch(sortOrder)
            {
                case "name":
                    query = query.OrderBy(h => h.Name);
                    break;
                case "beds_asc":
                    query = query.OrderBy(h => h.BedsCount);
                    break;
                case "beds_desc":
                    query = query.OrderByDescending(h => h.BedsCount);
                    break;
                default:
                    query = query.OrderBy(h => h.Name);
                    break;
            }
            return await query.ToListAsync();
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
