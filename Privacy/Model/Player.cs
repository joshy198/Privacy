using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    public class Player
    {
        [JsonProperty("id")]
        public ulong ID { get; set; }
        [JsonProperty("show_stat")]
        public bool  ShowStatistic { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
