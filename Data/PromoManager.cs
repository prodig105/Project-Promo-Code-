using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VladPromoCodeWebApp.Domain;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace VladPromoCodeWebApp.Data
{
    public class PromoManager
    {
        public List<Promo> Read()
        {

            List<Promo> list = new List<Promo>();
            Promo promo = new Promo();

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Promo>));
            using (FileStream file = new FileStream("PromoCodedata.txt", FileMode.OpenOrCreate))
            {

                list = ((List<Promo>)jsonFormatter.ReadObject(file));
            }
            return list;


        }
        public bool Save(List<Promo> list)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Promo>));
            using (FileStream fs = new FileStream("PromoCodedata.txt", FileMode.Truncate))
            {

                jsonFormatter.WriteObject(fs, list);

            }
            return true;
        }
        public void SaveToJson(List<Promo> list)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Promo>));
            using (FileStream fs = new FileStream("UserData.json", FileMode.Truncate))
            {

                jsonFormatter.WriteObject(fs, list);

            }
        }
    }
}
