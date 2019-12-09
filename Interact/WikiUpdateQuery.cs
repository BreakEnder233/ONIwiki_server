using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact
{
    public class WikiUpdateQuery
    {
        public readonly string hashedData;
        public readonly string password;
        public readonly string dataSetName;
        public readonly string dataString;

        public WikiUpdateQuery(string hashedData, string dataSetName, string dataString)
        {
            this.hashedData = hashedData;
            this.dataSetName = dataSetName;
            this.dataString = dataString;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
