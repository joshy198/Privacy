using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    /// <summary>
    /// Object of type Group containing the group's Id and the group's title in the user's language
    /// </summary>
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
