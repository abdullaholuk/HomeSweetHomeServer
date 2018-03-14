using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HomeSweetHomeServer.Models
{
    //BaseClass for any model which uses id number
    [Serializable]
    [DataContract]
    public class IdModel
    {
        [DataMember]
        public int Id { get; set; }
    }
}
