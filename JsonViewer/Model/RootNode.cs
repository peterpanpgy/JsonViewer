using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonViewer.Model
{
    public class RootNode : ObjectNode
    {
        public Schema Schema { get; private set; }

        public void FillSchema()
        {
            Schema = new Schema
            {
                Name = Name,
                NodeType = NodeType,
            };

            foreach (var node in Nodes)
            {
                node.FillSchema(Schema);
            }
        }

        public Node GetNodeFromPath(List<string> path)
        {
            if (NodeType == NodeType.Array)
                throw new ArgumentException("array is not expected.");

            Node current = null;
            foreach (var pathName in path)
            {
                if (current == null)
                {
                    if (pathName != Name) throw new ArgumentException("");
                    current = this;
                }
                else
                {
                    if (current.NodeType != NodeType.Object)
                        throw new ArgumentException("object is expected.");

                    var objectNode = current as ObjectNode;
                    current = objectNode.Nodes.FirstOrDefault(n => n.Name == pathName);
                    if (current == null)
                        throw new ArgumentException("node is expected.");
                }
            }

            return current;
        }

        public List<Node> GetNodes(Schema schema)
        {
            var nodes = GetNodesFromPath(schema.GetPath());
            if (schema.NodeType == NodeType.Array)
            {
                var allNodes = new List<Node>();
                foreach (var node in nodes.Where(n => n.NodeType == NodeType.Array))
                {
                    var arrayNode = node as ArrayNode;
                    if (arrayNode.Nodes.Count == 0) continue;
                    var itemNode = arrayNode.Nodes[0];
                    if (itemNode.NodeType == NodeType.Value || itemNode.NodeType == NodeType.Value)
                        allNodes.Add(itemNode);
                    else
                    {
                        allNodes.AddRange(arrayNode.Nodes);
                    }
                }
                return allNodes;
            }
            return nodes;
        }
    }
}
