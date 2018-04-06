using System;
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
