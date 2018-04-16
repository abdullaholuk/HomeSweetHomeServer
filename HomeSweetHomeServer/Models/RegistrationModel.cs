using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeSweetHomeServer.Models
{

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

        //[Required]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }
    }
}
