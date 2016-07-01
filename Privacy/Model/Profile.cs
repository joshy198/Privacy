using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    public class Profile
    {
        public ulong id { get; set; }
        public ulong points { get; set; }
        public string name { get; set; }
        public Language lang { get; set; }

        public override string ToString()
        {
            return String.Format("Name: {0}\nPoints: {1}\nLanguage: {2}",name,points,lang.title);
        }
    }
}
