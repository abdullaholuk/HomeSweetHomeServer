using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

//Models for action parameters at authentication controller 
namespace HomeSweetHomeServer.Models
{
    //Login information
    public class LoginModel
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    //Verification code information
    public class VerificationCodeModel
    {
        [Required]
        public string VerificationCode { get; set; }
    }

    //Registiration informations
    public class RegistrationModel
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }
    }

    //Forgot password information
    public class ForgotPasswordModel
    {
        [Required]
        public string VerificationCode { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
