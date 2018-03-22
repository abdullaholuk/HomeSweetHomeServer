using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace HomeSweetHomeServer.Models
{
    //BaseClass for any user
    [Serializable]
    [DataContract]
    public class UserModel
    {
        [Key]
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Username { get; set; }
    }
}
