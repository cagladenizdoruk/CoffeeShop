using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models.Model
{
    public class ChangePasswordModel
    {
        [Required]
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [DisplayName("New Password")]
        [StringLength(100,MinimumLength =5,ErrorMessage = "Your password must be at least 5 characters long")]
        public string NewPassword { get; set; }
        [Required]
        [DisplayName("Confirm New Password")]
        [Compare("NewPassword",ErrorMessage ="Passwords do not match")]
        public string ConNewPassword { get; set; }
    }
}