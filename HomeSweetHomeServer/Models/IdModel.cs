using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HomeSweetHomeServer.Models
{
    [Serializable]
    [DataContract]
    public class IdModel
    {
        [DataMember]
        public int Id { get; set; }
    }
}
