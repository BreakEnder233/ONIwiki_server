using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Interact
{
    public class WikiQueryResponse
    {
        //public readonly string hashedData;
        public readonly string dataSetName;
        public readonly WikiResponseStatus status;
        public readonly string dataString;

        public WikiQueryResponse(string dataSetName, WikiResponseStatus status, string dataString)
        {
            this.dataSetName = dataSetName;
            this.status = status;
            this.dataString = dataString;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
