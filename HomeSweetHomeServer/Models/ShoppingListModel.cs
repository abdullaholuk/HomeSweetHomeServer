using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeSweetHomeServer.Models
{
    [Serializable]
    [DataContract]
    public class ShoppingListModel
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [ForeignKey("MaterialId")]
        public MaterialModel Material { get; set; }

        [ForeignKey("HomeId")]
        public HomeModel Home { get; set; }

        [DataMember]
        public int Status { get; set; }
    }
}
