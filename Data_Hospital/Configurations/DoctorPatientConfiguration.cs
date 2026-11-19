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
    public class DoctorPatientConfiguration : IEntityTypeConfiguration<DoctorPatient>
    {
        public void Configure(EntityTypeBuilder<DoctorPatient> builder)
        {
          
            builder.HasKey(dp => new { dp.DoctorId, dp.PatientId });

            builder.HasOne(dp => dp.Doctor)
                .WithMany(d => d.Patients)
                .HasForeignKey(dp => dp.DoctorId)
                .OnDelete(DeleteBehavior.Cascade); // za da se iztriqt zapisite v i dvete tablici

            builder.HasOne(dp => dp.Patient)
                .WithMany(p => p.Doctors)
                .HasForeignKey(dp => dp.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new DoctorPatient { DoctorId = 1, PatientId = 1 },
                new DoctorPatient { DoctorId = 1, PatientId = 2 },
                new DoctorPatient { DoctorId = 2, PatientId = 1 },
                new DoctorPatient { DoctorId = 2, PatientId = 3 },
                new DoctorPatient { DoctorId = 3, PatientId = 4 },
                new DoctorPatient { DoctorId = 4, PatientId = 5 },
                new DoctorPatient { DoctorId = 4, PatientId = 3 }
            );
        }
    }
}
