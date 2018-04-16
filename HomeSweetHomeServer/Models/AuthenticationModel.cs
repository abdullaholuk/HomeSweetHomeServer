using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HomeSweetHomeServer.Models
{
    //Keeps user authentication informations
    [Serializable]
    [DataContract]
    public class AuthenticationModel : UserModel
    {
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public bool IsVerifiedByEmail { get; set; }
    }
}
