﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModel
{
    public class LoginModel
    {
       
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
