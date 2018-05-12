using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace HomeSweetHomeServer.Models
{
    //Expense information
    [Serializable]
    [DataContract]
    public class ExpenseModel
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int EType { get; set; }

        [DataMember]
        public double Cost { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Content { get; set; }
    }
   
    public enum ExpenseType
    {
        HouseRent = 1,
        Bill = 2,
        Shopping = 3,
        TransferMoney = 4,
        Others = 5
    }
}
