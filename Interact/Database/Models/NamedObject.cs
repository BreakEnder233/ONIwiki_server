using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.Database.Models
{
    public class NamedObject
    {
        public string name;
        public string normalizedName;
        public string description;
        public List<NamedObject> children;
    }
}
