using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace HomeSweetHomeServer.Models
{
    //Makes a brigde between user and information
    [Serializable]
    [DataContract]
    public class UserInformationModel
    {
        [Key]
        [DataMember]
        public int UserInformationId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int InformationId { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
