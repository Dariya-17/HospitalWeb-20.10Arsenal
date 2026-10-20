using System.ComponentModel.DataAnnotations;

namespace HospitalWeb.ViewModels.Patient
{
    public class PatientIndexViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DoctorName { get; set; } = string.Empty;
    }
}
