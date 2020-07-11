using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaFor_Demo
{
    public class Metadata
    {
        public string file;
        public string type;
        public string metadata;

        public Metadata(string file, string type, string metadata)
        {
            this.file = file;
            this.type = type;
            this.metadata = metadata;
        }

        
    }
}
