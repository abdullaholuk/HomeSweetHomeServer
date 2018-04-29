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

        [Required]
        public string DeviceId { get; set; }
    }

    //Verification code information
    public class VerificationCodeModel
    {
        [Required]
        public string VerificationCode { get; set; }

        [Required]
        public int UserId { get; set; }
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
        public string Email { get; set; }

        [Required]
        public string VerificationCode { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }

    //Basic user informations
    public class UserBaseModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserBaseModel()
        {

        }

        public UserBaseModel(int id, string username, string firstName, string lastName)
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
        }

    }

    //User's full information
    public class UserFullInformationModel
    {
        public UserBaseModel User { get; set; }

        public string Token { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Position { get; set; }

        public int HomeId { get; set; }

        public string HomeName { get; set; }

        public List<UserBaseModel> Friends { get; set; }

        public UserFullInformationModel()
        {
            User = new UserBaseModel();
            Friends = new List<UserBaseModel>();
        }

    }
}
