using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Let_s_Eat_Bee_Project.Models
{
    public class ReportViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Адрес электронной почты")]
        public string Email { set; get; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Пароль")]
        public string Pass { set; get; }
    }
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display (Name ="Last Name")]
        public string LastName { get; set; }
        public string Organization { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}
