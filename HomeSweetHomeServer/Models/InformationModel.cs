using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeSweetHomeServer.Models
{
    //Keeps knowlegde about a single information
    [Serializable]
    [DataContract]
    public class InformationModel
    {
        [Key]
        [DataMember]
        public int InformationId { get; set; }

        [DataMember]
        public string InformationName { get; set; }

        [DataMember]
        public string InformationType { get; set; }
    }
}
