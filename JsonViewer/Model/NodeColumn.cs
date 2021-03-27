using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonViewer.Model
{
    public class NodeColumn
    {
        public string Name { get; set; }
        public NodeType NodeType { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
