using Data_Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Hospital.Configurations
{
    public class HospitalConfiguration:IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasData(
                new Hospital
                {
                    Id = 1,
                    Name = "MBAL Kazanluk",
                    Address = "Kazanluk",
                    Email = "hospital@email.com",
                    PhoneNumber = "123-456-7890",
                    BedsCount = 100,
                    CreatedOn = new DateTime(2024, 6, 1),
                    IsDeleted = false
                },
                new Hospital
                {
                    Id = 2,
                    Name = "Umbal Stara Zagora",
                    Address = "Stara Zagora",
                    Email = "countyhospital@email.com",
                    PhoneNumber = "987-654-3210",
                    BedsCount = 150,
                    CreatedOn = new DateTime(2026, 1 , 1),
                    IsDeleted = false
                }
            );
        }
    }
}
