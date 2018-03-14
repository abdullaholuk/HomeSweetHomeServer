using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HomeSweetHomeServer.Models
{
    //Keeps friendship informations
    [Serializable]
    [DataContract]
    public class FriendshipModel : IdModel
    {
        [DataMember]
        public int User1Id { get; set; }

        [DataMember]
        public int User2Id { get; set; }

        [DataMember]
        public int Debt { get; set; }
    }
}
