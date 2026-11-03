using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data_Hospital.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "BedsCount", "CreatedOn", "Email", "IsDeleted", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Kazanluk", 100, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hospital@email.com", false, "MBAL Kazanluk", "123-456-7890" },
                    { 2, "Stara Zagora", 150, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "countyhospital@email.com", false, "Umbal Stara Zagora", "987-654-3210" },
                    { 3, "Plovdiv", 200, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "plovdivhospital@email.com", false, "MBAL Plovdiv", "032-555-1234" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "petar.stoyanov@email.com", "Petar", "Stoyanov", "0899111222" },
                    { 2, "nikolina.koleva@email.com", "Nikolina", "Koleva", "0899222333" },
                    { 3, "stefan.todorov@email.com", "Stefan", "Todorov", "0899333444" },
                    { 4, "desislava.nikolova@email.com", "Desislava", "Nikolova", "0899444555" },
                    { 5, "hristo.angelov@email.com", "Hristo", "Angelov", "0899555666" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Email", "FirstName", "HospitalId", "LastName", "PhoneNumber", "Specialty" },
                values: new object[,]
                {
                    { 1, "ivan.petrov@email.com", "Ivan", 1, "Petrov", "0888123456", "Cardiology" },
                    { 2, "maria.georgieva@email.com", "Maria", 1, "Georgieva", "0888234567", "Neurology" },
                    { 3, "georgi.dimitrov@email.com", "Georgi", 2, "Dimitrov", "0888345678", "Surgery" },
                    { 4, "elena.ivanova@email.com", "Elena", 3, "Ivanova", "0888456789", "Pediatrics" }
                });

            migrationBuilder.InsertData(
                table: "DoctorPatients",
                columns: new[] { "DoctorId", "PatientId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 3 },
                    { 3, 4 },
                    { 4, 3 },
                    { 4, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DoctorPatients",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DoctorPatients",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "DoctorPatients",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "DoctorPatients",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "DoctorPatients",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "DoctorPatients",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "DoctorPatients",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
