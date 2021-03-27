using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonViewer.Model
{
    public class ArrayNode : ObjectNode
    {
        public override NodeType NodeType => NodeType.Array;
        public override string DisplayName => $"Name [${Nodes.Count}]";

        public override string DisplayValue(NodeViewOption nodeViewOption)
        {
            return "<array>";
        }

        public override void FillSchema(Schema schema)
        {
            var subSchema = GetSchema(schema);
            foreach (var node in Nodes)
            {
                if (node is ObjectNode objectNode)
                {
                    foreach (var subNode in objectNode.Nodes)
                    {
                        subNode.FillSchema(subSchema);
                    }
                }
            }
        }
    }
}
