using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace HomeSweetHomeServer.Models
{
    //Meal information 
    [Serializable]
    [DataContract]
    public class MealModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Ingredients { get; set; }

        [DataMember]
        public string Note { get; set; }

        [ForeignKey("MenuId")]
        public MenuModel Menu { get; set; }
    }
}
