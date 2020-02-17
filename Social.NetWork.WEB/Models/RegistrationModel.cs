using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Social.NetWork.WEB.Models {
    public class RegistrationModel {
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} должен быть не менее {2} значной длинны.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароль и пароль подтверждения не совпадают.")]
        public string ConfirmPassword { get; set; }
        [Display(Name ="О себе..")]
        public string AboutMe { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}