using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HomeSweetHomeServer.Models
{
    [Serializable]
    [DataContract]
    public class UserInformationModel
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string InformationId { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
