using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HomeSweetHomeServer.Models
{
    //Keeps information about synchronization
    [Serializable]
    [DataContract]
    public class SyncModel
    {
        [DataMember]
        [Required]
        public int Id { get; set; }

        [DataMember]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdated { get; set; }
    }

    //Notepad synchronization
    [Serializable]
    [DataContract]
    public class ClientNotepadContextModel
    {
        [DataMember]
        [Required]
        public List<SyncModel> LastState;
    }
}
