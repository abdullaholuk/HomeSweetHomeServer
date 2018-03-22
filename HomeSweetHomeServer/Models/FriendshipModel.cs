using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace HomeSweetHomeServer.Models
{
    //Keeps friendship informations
    [Serializable]
    [DataContract]
    public class FriendshipModel
    {
        [Key]
        [DataMember]
        public int FriendshipId { get; set; }

        [DataMember]
        public int User1Id { get; set; }

        [DataMember]
        public int User2Id { get; set; }

        [DataMember]
        public double Debt { get; set; }
    }
}
