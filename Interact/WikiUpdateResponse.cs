using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact
{
    public class WikiUpdateResponse
    {
        public readonly string dataSetName;
        public readonly WikiResponseStatus status;

        public WikiUpdateResponse(string dataSetName, WikiResponseStatus status)
        {
            this.dataSetName = dataSetName;
            this.status = status;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
