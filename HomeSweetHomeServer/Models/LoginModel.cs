using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeSweetHomeServer.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
