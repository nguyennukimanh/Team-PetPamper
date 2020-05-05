using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetPamper.Areas.Admin.Models
{
    public class ResetPasswordModel
    {
        [EmailAddress(ErrorMessage = "Đây không phải là email")]
        [Required(ErrorMessage = "Email là bắt buộc")]
        public string Email { get; set; }
    }
}