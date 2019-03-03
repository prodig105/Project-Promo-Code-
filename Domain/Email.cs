using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;
namespace VladPromoCodeWebApp.Domain
{
    [DataContract]
    public class Email
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        [DataMember]

        public string Email1 { get; set; }
        [DataMember]
        public string SaleCode { get; set; }

    }
}
