using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    /// <summary>
    /// Object of type Question, containing the question's id and the title in the user's language
    /// </summary>
    public class Question
    {
        [JsonProperty("id")]
        public UInt64 ID { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
