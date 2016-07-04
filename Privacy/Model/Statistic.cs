using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    public class Statistic
    {
        [JsonProperty("id")]
        public ulong ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("points")]
        public int Points { get; set; }
        [JsonProperty("guessed")]
        public int Guessed { get; set; }
        [JsonProperty("yeses")]
        public int Yeses { get; set; }
    }
}
