using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModel
{
    public class ResetPasswordModel
    {

        [Required]
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        //[RegularExpression("^(?=.?[A-Z])(?=.?[a-z])(?=.?[0-9])(?=.?[#?!@$ %^&*-]).{8,}$", ErrorMessage = "Password is not valid. Password Should be 8 Character contain 1 Uppercase, 1 Special character, 1 Number")]
        public string NewPassword { get; set; }
    }
}
