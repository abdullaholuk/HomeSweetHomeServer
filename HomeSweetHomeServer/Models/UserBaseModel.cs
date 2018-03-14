using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HomeSweetHomeServer.Models
{
    //BaseClass for any user
    [Serializable]
    [DataContract]
    public class UserBaseModel : IdModel
    {
    }
}
