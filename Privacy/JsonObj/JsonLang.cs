using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.JsonObj
{
    public class JsonLang
    {
        [JsonProperty("LANGUAGES")]
        public IEnumerable<Language> Langs { get; set; }
    }
}
