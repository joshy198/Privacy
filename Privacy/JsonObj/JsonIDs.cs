using Newtonsoft.Json;
using Privacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.JsonObj
{
    public class JsonIDs
    {
        [JsonProperty("IDS")]
        public IEnumerable<ID> QuestionIDs { get; set; }
        private Random rng = new Random();
    }
}
