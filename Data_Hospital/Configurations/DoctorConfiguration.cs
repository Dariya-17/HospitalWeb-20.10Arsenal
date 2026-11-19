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
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(d => d.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(d => d.Specialty)  
                .IsRequired()
                    .HasMaxLength(100);

            builder.Property(d => d.Email)
                .IsRequired();
            builder.Property(d => d.PhoneNumber)
                .IsRequired();
               
            builder.HasOne(d => d.Hospital)
                .WithMany(h => h.Doctors)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(
                new Doctor
                {
                    Id = 1,
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    Specialty = "Cardiology",
                    Email = "ivan.petrov@email.com",
                    PhoneNumber = "0888123456",
                    HospitalId = 1

                },
                new Doctor
                {
                    Id = 2,
                    FirstName = "Maria",
                    LastName = "Georgieva",
                    Specialty = "Neurology",
                    Email = "maria.georgieva@email.com",
                    PhoneNumber = "0888234567",
                    HospitalId = 1
                },
                new Doctor
                {
                    Id = 3,
                    FirstName = "Georgi",
                    LastName = "Dimitrov",
                    Specialty = "Surgery",
                    Email = "georgi.dimitrov@email.com",
                    PhoneNumber = "0888345678",
                    HospitalId = 2
                },
                new Doctor
                {
                    Id = 4,
                    FirstName = "Elena",
                    LastName = "Ivanova",
                    Specialty = "Pediatrics",
                    Email = "elena.ivanova@email.com",
                    PhoneNumber = "0888456789",
                    HospitalId = 3
                }
            );
        }
    }
}
