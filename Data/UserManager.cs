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
    public class UserManager
    {
        
        public List<User> Read()
        {

            List<User> list = new List<User>();
            User email = new User();

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<User>));
            using (FileStream file = new FileStream("UserData.txt", FileMode.OpenOrCreate))
            {
               
                list=((List<User>)jsonFormatter.ReadObject(file));
            }
            return list;


        }
        public bool Save(List<User> list)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<User>));
            using (FileStream fs = new FileStream("UserData.txt", FileMode.Truncate))
            {
                
                jsonFormatter.WriteObject(fs, list);

            }
            return true;
        }
        public void SaveToJson(List<User> list)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<User>));
            using (FileStream fs = new FileStream("UserData.json", FileMode.Truncate))
            {

                jsonFormatter.WriteObject(fs, list);

            }
        }
    }
}
