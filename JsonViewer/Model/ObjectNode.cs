using JsonViewer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace JsonViewer.Model
{
    public class ObjectNode : Node
    {
        public List<Node> Nodes { get; } = new List<Node>();
        public override NodeType NodeType => NodeType.Object;
        public override string DisplayName => Name;

        public override string DisplayValue(NodeViewOption nodeViewOption)
        {
            return IsSimple(nodeViewOption) ? GetString(nodeViewOption) : "<object>";
        }

        public string GetString(NodeViewOption nodeViewOption)
        {
            var nodeValues = GetValues(nodeViewOption);
            var value = "{ ";
            for (var i = 0; i < Nodes.Count; ++i)
            {
                var node = Nodes[i];
                var nodeValue = node.Name + ": " + nodeValues[i];
                if (i == 0) value += nodeValue;
                else value += ", " + nodeValue;
            }
            value += " }";
            return value;
        }

        private List<string> GetValues(NodeViewOption nodeViewOption)
        {
            if (NodeUtility.IsPoint(this) 
                && nodeViewOption.ShowGlobalPoint
                && Parent != null && !NodeUtility.IsCoordinateSystem(Parent as ObjectNode))
            {
                try
                {
                    var matrix3D = Matrix3D.Identity;
                    var current = Parent as ObjectNode;
                    while (current != null)
                    {
                        if (current.Nodes.FirstOrDefault(n => NodeUtility.IsCoordinateSystem(n as ObjectNode)) is ObjectNode coodinateSystemNode)
                        {
                            var coordinateSystem = NodeUtility.CoordinateSystemConvertFromObjectNode(coodinateSystemNode);
                            matrix3D *= coordinateSystem.TransformationMatrixTo();
                        }
                        current = current.Parent as ObjectNode;
                    }
                    var point = NodeUtility.Point3DConvertFromObjectNode(this) * matrix3D;
                    return new List<string>
                    {
                        point.X.ToString(),
                        point.Y.ToString(),
                        point.Z.ToString()
                    };
                }
                catch(Exception)
                {
                }
            }
            return Nodes.Select(n => (n as ValueNode).Value).ToList();
        }

        public bool IsSimple(NodeViewOption nodeViewOption)
        {
            return Nodes.Count <= nodeViewOption.SimpleCount
                && Nodes.All(n => n.NodeType == NodeType.Value);
        }

        public override void FillSchema(Schema schema)
        {
            var subSchema = GetSchema(schema);
            foreach (var node in Nodes)
            {
                node.FillSchema(subSchema);
            }
        }

        protected Schema GetSchema(Schema schema)
        {
            var subSchema = schema.SubSchemas.FirstOrDefault(s => s.Name == Name);
            if (subSchema == null)
            {
                subSchema = new Schema
                {
                    Parent = schema,
                    Name = Name,
                    NodeType = NodeType,
                };
                schema.SubSchemas.Add(subSchema);
            }
            return subSchema;
        }

        public List<Node> GetNodesFromPath(List<string> path)
        {
            var nodes = new List<Node>();
            var nodePath = GetPath();
            if (IsSamePath(nodePath, path))
            {
                nodes.Add(this);
                return nodes;
            }

            var objectNodes = new List<ObjectNode>();
            foreach (var node in Nodes)
            {
                nodePath = node.GetPath();
                if (IsSamePath(nodePath, path))
                    nodes.Add(node);
                else if (node is ObjectNode objectNode
                    && ContainPath(path, nodePath))
                {
                    objectNodes.Add(objectNode);
                }
            }

            foreach (var objectNode in objectNodes)
            {
                var subNodes = objectNode.GetNodesFromPath(path);
                nodes.AddRange(subNodes);
            }

            return nodes;
        }
    }
}
