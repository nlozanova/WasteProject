using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wastes.ViewModels
{
    public class ProfileVM
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Емайл")]
        public string Email { get; set; }

        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}