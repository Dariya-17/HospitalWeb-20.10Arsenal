using Data_Hospital.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Library.Interfaces
{
   public interface IHospitalService
    {
        Task<List<Hospital>> GetAllHospitals(string?search=null,int? minBeds=null,string ?sortOrder=null);
        Task<Hospital?> GetHospitalById(int id);
        Task AddHospitalAsync(Hospital hospital);
        Task UpdateHospitalAsync(Hospital hospital);
        Task DeleteHospitalAsync(int id);

    }
}
