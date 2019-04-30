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
        
            public List<Promo> Read(int i)
            {

                List<Promo> list = new List<Promo>();
                Promo promo = new Promo();

                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Promo>));
          
            using (StreamReader file = new StreamReader("PromoCodedata.json"))
            {

                var content = file.ReadToEnd();
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
                stream.Position = 0;
                list = ((List<Promo>)jsonFormatter.ReadObject(stream));
                //list = (List<Promo>)JsonConvert.DeserializeObject(content);
            }

            return list;
            }
          
            public bool Save(List<Promo> list,int i)
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Promo>));
            using (StreamWriter fs = new StreamWriter("PromoCodedata.json"))
                {
                var JSON = JsonConvert.SerializeObject(list);
                fs.Write(JSON);


            }
                return true;
            }
         

           public List<Promo> Read()
        {

            List<Promo> list = new List<Promo>();

            string json = File.ReadAllText("PromoCodedata.json",Encoding.UTF8);
            list = ((List < Promo > )JsonConvert.DeserializeObject(json));

            return list;
        }

        public void Save(List<Promo> list)
        {
            string output = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText("settings.json", output,Encoding.UTF8);
        }
    }
    }
