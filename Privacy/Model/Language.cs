using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy
{
    /// <summary>
    /// Object of type Language, containing the language's id and it's title
    /// </summary>
    public class Language
    {
        [JsonProperty("id")]
        public UInt64 Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
}
