using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    /// <summary>
    /// object of type player containing the player's id and the players name
    /// </summary>
    public class Player
    {
        [JsonProperty("id")]
        public ulong ID { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
