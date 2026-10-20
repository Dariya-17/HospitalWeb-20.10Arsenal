using System.ComponentModel.DataAnnotations;

namespace HospitalWeb.ViewModels.Hospital
{
    public class HospitalEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Името е задължителен")]
        [StringLength(50, ErrorMessage = "Максимум 50 символа")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Адресът е задължителен")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Имейлът е задължителен")]

        [EmailAddress(ErrorMessage = "Невалиден имейл")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Телефонът е задължителен")]

        [Phone(ErrorMessage = "Невалиден телефон")]
        public string PhoneNumber { get; set; }

        [Range(1, 5000, ErrorMessage = "Броят легла трябва да е между 1 и 5000")]
        public int BedsCount { get; set; }
    }
}
