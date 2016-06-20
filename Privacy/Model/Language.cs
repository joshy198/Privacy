using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy
{
    public class Language
    {
        public UInt64 id { get; set; }
        public string title { get; set; }
        public override string ToString()
        {
            return title;
        }
    }
}
