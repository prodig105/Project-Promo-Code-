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
    public class EmailManager
    {
        
        public List<Email> Read()
        {

            List<Email> list = new List<Email>();
            Email email = new Email();

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Email>));
            using (FileStream file = new FileStream("Data.txt", FileMode.OpenOrCreate))
            {
               
                list=((List<Email>)jsonFormatter.ReadObject(file));
            }
            return list;


        }
        public bool Save(List<Email> list)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Email>));
            using (FileStream fs = new FileStream("Data.txt", FileMode.Truncate))
            {

                jsonFormatter.WriteObject(fs, list);

            }
            return true;
        }
        public void SaveToJson(List<Email> list)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Email>));
            using (FileStream fs = new FileStream("Data.json", FileMode.Truncate))
            {

                jsonFormatter.WriteObject(fs, list);

            }
        }
    }
}
