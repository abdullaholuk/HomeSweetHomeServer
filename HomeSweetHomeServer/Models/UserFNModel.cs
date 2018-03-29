using System;
using System.Runtime.Serialization;

namespace HomeSweetHomeServer.Models
{
    //Keeps information about user's name for client return
    [Serializable]
    [DataContract]
    public class UserFNModel : UserModel
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }
}
