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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(d => d.Email)
               .IsRequired();
            builder.Property(d => d.PhoneNumber)
                .IsRequired();


            builder.HasData(
                new Patient
                {
                    Id = 1,
                    FirstName = "Petar",
                    LastName = "Stoyanov",
                    Email = "petar.stoyanov@email.com",
                    PhoneNumber = "0899111222"
                },
                new Patient
                {
                    Id = 2,
                    FirstName = "Nikolina",
                    LastName = "Koleva",
                    Email = "nikolina.koleva@email.com",
                    PhoneNumber = "0899222333"
                },
                new Patient
                {
                    Id = 3,
                    FirstName = "Stefan",
                    LastName = "Todorov",
                    Email = "stefan.todorov@email.com",
                    PhoneNumber = "0899333444"
                },
                new Patient
                {
                    Id = 4,
                    FirstName = "Desislava",
                    LastName = "Nikolova",
                    Email = "desislava.nikolova@email.com",
                    PhoneNumber = "0899444555"
                },
                new Patient
                {
                    Id = 5,
                    FirstName = "Hristo",
                    LastName = "Angelov",
                    Email = "hristo.angelov@email.com",
                    PhoneNumber = "0899555666"
                }
            );
        }
    }
}
