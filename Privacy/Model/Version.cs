using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    public class PckVersion
    {
            [JsonProperty("vnr")]
            public double VersionNr { get; set; }
            public override string ToString()
            {
                return VersionNr.ToString();
            }
    }
}
