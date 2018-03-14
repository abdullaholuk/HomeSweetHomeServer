using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HomeSweetHomeServer.Models
{
    //Keeps information about keys
    [Serializable]
    [DataContract]
    public class KeyModel : IdModel
    {
        [DataMember]
        public string KeyName { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
