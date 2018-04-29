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
        public int Id { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public HomeModel Home { get; set; }

        [DataMember]
        public string DeviceId { get; set; }
    }
}
