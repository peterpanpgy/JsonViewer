using JsonViewer.Utility;
using System;
using System.Linq;
using System.Windows.Media.Media3D;

namespace JsonViewer.Model
{
    public static class NodeUtility
    {
        public static bool IsPoint(ObjectNode objectNode)
        {
            if (objectNode.Nodes.Count != 3) return false;
            return objectNode.Nodes[0].Name.ToLower() == "x"
                && objectNode.Nodes[1].Name.ToLower() == "y"
                && objectNode.Nodes[2].Name.ToLower() == "z";
        }

        public static bool IsCoordinateSystem(ObjectNode objectNode)
        {
            if (objectNode == null) return false;

            if (objectNode.Nodes.Count != 3) return false;
            if (!objectNode.Nodes.All(n => n is ObjectNode)) return false;

            var subNodes = objectNode.Nodes.OfType<ObjectNode>().ToList();
            if (!subNodes.All(n => n.Nodes.Count == 3)) return false;

            var origin = subNodes[0];
            var xaxis = subNodes[1];
            var yaxis = subNodes[2];

            if (origin.Name.ToLower() != "origin"
                || xaxis.Name.ToLower() != "xaxis"
                || yaxis.Name.ToLower() != "yaxis")
            {
                return false;
            }

            return origin.Nodes[0].Name.ToLower() == "x"
                && origin.Nodes[1].Name.ToLower() == "y"
                && origin.Nodes[2].Name.ToLower() == "z"
                && xaxis.Nodes[0].Name.ToLower() == "x"
                && xaxis.Nodes[1].Name.ToLower() == "y"
                && xaxis.Nodes[2].Name.ToLower() == "z"
                && yaxis.Nodes[0].Name.ToLower() == "x"
                && yaxis.Nodes[1].Name.ToLower() == "y"
                && yaxis.Nodes[2].Name.ToLower() == "z";
        }

        public static CoordinateSystem CoordinateSystemConvertFromObjectNode(ObjectNode objectNode)
        {
            var origin = objectNode.Nodes[0] as ObjectNode;
            var xaxis = objectNode.Nodes[1] as ObjectNode;
            var yaxis = objectNode.Nodes[2] as ObjectNode;

            return new CoordinateSystem
            {
                Origin = new Point3D
                {
                    X = Convert.ToDouble((origin.Nodes[0] as ValueNode).Value),
                    Y = Convert.ToDouble((origin.Nodes[1] as ValueNode).Value),
                    Z = Convert.ToDouble((origin.Nodes[2] as ValueNode).Value),
                },

                Xaxis = new Point3D
                {
                    X = Convert.ToDouble((xaxis.Nodes[0] as ValueNode).Value),
                    Y = Convert.ToDouble((xaxis.Nodes[1] as ValueNode).Value),
                    Z = Convert.ToDouble((xaxis.Nodes[2] as ValueNode).Value),
                },

                Yaxis = new Point3D
                {
                    X = Convert.ToDouble((yaxis.Nodes[0] as ValueNode).Value),
                    Y = Convert.ToDouble((yaxis.Nodes[1] as ValueNode).Value),
                    Z = Convert.ToDouble((yaxis.Nodes[2] as ValueNode).Value),
                },
            };
        }

        public static Point3D Point3DConvertFromObjectNode(ObjectNode objectNode)
        {
            return new Point3D
            {
                X = Convert.ToDouble((objectNode.Nodes[0] as ValueNode).Value),
                Y = Convert.ToDouble((objectNode.Nodes[1] as ValueNode).Value),
                Z = Convert.ToDouble((objectNode.Nodes[2] as ValueNode).Value),
            };
        }
    }
}
