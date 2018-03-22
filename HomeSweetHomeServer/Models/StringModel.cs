using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace HomeSweetHomeServer.Models
{
    //Keeps information about keys
    [Serializable]
    [DataContract]
    public class StringModel
    {
        [Key]
        [DataMember]
        public int StringId { get; set; }

        [DataMember]
        public string StringName { get; set; }

        [DataMember]
        public string StringValue { get; set; }
    }
}
