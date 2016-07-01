using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    public class ID
    {
        public UInt64 id { get; set; }
        public override string ToString()
        {
            return id.ToString();
        }
    }
}
