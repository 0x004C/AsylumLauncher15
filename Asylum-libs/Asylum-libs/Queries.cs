using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsylumLibs
{
    public class Queries
    {
        public static Query LAUNCHER_VERSION = new Query("launcher_version");
        public static Query ASYLUM_VERSION = new Query("asylum_version");
        public static Query ACCOUNT = new Query("account");
    }

    public class Query
    {
        public string Key = "";
        public string[] Parameters = null;

        public Query(string key)
        {
            this.Key = key;
        }
    }
}
