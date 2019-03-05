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
            using (StreamReader file = new StreamReader("UserData.json"))
            {

                var content = file.ReadToEnd();
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
                stream.Position = 0;
                list = ((List<User>)jsonFormatter.ReadObject(stream));
                return list;
            }

        }
        public bool Save(List<User> list)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<User>));
            using (StreamWriter fs = new StreamWriter("UserData.json"))
            {
                var JSON = JsonConvert.SerializeObject(list);
                fs.Write(JSON);


            }
            return true;
        }

    }
}
