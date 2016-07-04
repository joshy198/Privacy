using Newtonsoft.Json;
using Privacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.JsonObj
{
    public class JsonGroup
    {
        [JsonProperty("CATEGORY")]
        public IEnumerable<Group> QouestionGroups { get; set; }
    }
}
