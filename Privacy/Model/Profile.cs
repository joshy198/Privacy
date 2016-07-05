using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    /// <summary>
    /// Object of type Profile containing the user's id, his points, his name, and his language as an object of type language
    /// </summary>
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
