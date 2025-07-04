﻿using System.ComponentModel.DataAnnotations;

namespace IdentityApp.Models.Account
{

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
