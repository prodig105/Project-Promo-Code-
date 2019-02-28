using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace VladPromoCodeWebApp.Controllers
{
    public class PromoCodeGeneration
    {
        Random rand = new Random();
        private string code;
        private string[] arrCode =
        {
              "1","2","3","4","5","6","7","8","9","0","Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","Z",
              "X","C","V","B","N","M"
        };
        public string PromoCode()
        {
            for (int i = 0; i <= 16; i++)
            {
                code += arrCode[rand.Next(36)];
            }
            return code;
        }

        public void PushEmail(string Email)
        {
            File.WriteAllText("Email.txt",Email);
        }
    }
}
