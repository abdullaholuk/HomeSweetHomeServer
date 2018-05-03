using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HomeSweetHomeServer.Models
{
    //Sychronize response model for entities
    [Serializable]
    [DataContract]
    public class SynchronizeModel<Tentity> where Tentity : class
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Operation { get; set; }

        [DataMember]
        public Tentity Entity { get; set; }

        public SynchronizeModel()
        {

        }

        public SynchronizeModel(int id, int operation, Tentity entity)
        {
            Id = id;
            Operation = operation;
            Entity = entity;
        }
    }
}
