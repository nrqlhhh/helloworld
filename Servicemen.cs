using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fyp.Models
{
    public class Servicemen

    {
        [Required(ErrorMessage = "Please enter NRIC")]
        [Remote(action: "VerifyUserID", controller: "Account")]
        public string NRIC { get; set; }

       

        public string UserRole { get; set; }

        public DateTime LastLogin { get; set; }
    }
}