using Newtonsoft.Json;
using Privacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.JsonObj
{
    public class JsonStat
    {
        [JsonProperty("STAT")]
        public IEnumerable<Statistic> Statistics { get; set; }
    }
}
