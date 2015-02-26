using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace AsylumLibs
{
    public class Account
    {
        public string AccountName;
        public string Password;
        public string Token;
        public string IGN;

        public Account(string accountname)
        {
            this.AccountName = accountname;
        }

        public string Serialize(bool doFormat)
        {
            if (doFormat)
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            else
                return JsonConvert.SerializeObject(this, Formatting.None);

        }

        public string Serialize()
        {
            return Serialize(false);
        }

        public static Account Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Account>(json);
        }
    }
}
