using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HomeSweetHomeServer.Models
{
    [Serializable]
    [DataContract]
    public class InformationModel : IdModel
    {
        [DataMember]
        public string InformationName { get; set; }

        [DataMember]
        public string InformationType { get; set; }
    }
}
