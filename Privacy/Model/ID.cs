using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    /// <summary>
    /// Object of type ID containing an ID
    /// </summary>
    public class ID
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
