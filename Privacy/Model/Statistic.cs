using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    /// <summary>
    /// Object of type Statistic, containing the id of the player, the name of the player, the players points the difference of the players' guess, the actual yes-votes
    /// </summary>
    public class Statistic
    {
        [JsonProperty("id")]
        public ulong ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("points")]
        public int Points { get; set; }
        [JsonProperty("difference")]
        public int Difference { get; set; }
        [JsonProperty("yesses")]
        public int Yesses { get; set; }
    }
}
