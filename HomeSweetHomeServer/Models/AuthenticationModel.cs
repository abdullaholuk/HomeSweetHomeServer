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
    public class AuthenticationModel : UserBaseModel
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
