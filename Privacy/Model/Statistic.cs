using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    public class Statistic
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public int points { get; set; }
        public int guessed { get; set; }
        public int yeses { get; set; }
    }
}
