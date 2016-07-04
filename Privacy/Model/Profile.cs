using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    public class Profile
    {
        [JsonProperty("id")]
        public ulong ID { get; set; }
        [JsonProperty("points")]
        public ulong Points { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lang")]
        public Language Lang { get; set; }

        public override string ToString()
        {
            return String.Format("Name: {0}\nPoints: {1}\nLanguage: {2}",Name,Points,Lang.Title);
        }
    }
}
