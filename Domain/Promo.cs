using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;
namespace VladPromoCodeWebApp.Domain
{
    public class Promo
    {
        [DataMember]
        public string SaleCode { get; set; }

        [DataType(DataType.EmailAddress)]
        [DataMember]
        public string Email1 { get; set; }
    }
}
