using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    public class Group
    {
        [JsonProperty("id")]
        public UInt64 ID { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
}
