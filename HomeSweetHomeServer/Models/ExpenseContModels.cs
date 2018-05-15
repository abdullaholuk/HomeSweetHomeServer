using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace HomeSweetHomeServer.Models
{
    //Client request expense model
    [Serializable]
    [DataContract]
    public class ClientExpenseModel
    {
        [DataMember]
        [Required]
        public ExpenseModel Expense { get; set; }

        [DataMember]
        public List<int> Participants { get; set; }
    }
}
