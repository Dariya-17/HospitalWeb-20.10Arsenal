using Data_Hospital;
using Hospital_Library.Interfaces;
using Hospital_Library.Services;
using Hospital_ManagerCore.Interfaces;
using Hospital_ManagerCore.Services;
using Microsoft.EntityFrameworkCore;

namespace HospitalWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<HospitalDbContext>(options =>
            options.UseSqlServer(builder.Configuration
            .GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IHospitalService,HospitalServices>();
            builder.Services.AddScoped<IDoctorService,DoctorServices>();
            builder.Services.AddScoped<IPatientServices, PatientServices>();

            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Hospital}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
