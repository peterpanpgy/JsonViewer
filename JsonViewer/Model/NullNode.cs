using System.Collections.Generic;

namespace JsonViewer.Model
{
    public class NullNode : Node
    {
        public override NodeType NodeType => NodeType.Null;
        public override string DisplayName => Name;

        public override string DisplayValue(NodeViewOption nodeViewOption)
        {
            return Parent != null && Parent.NodeType == NodeType.Array
                ? (Parent as ObjectNode).GetString(nodeViewOption) : "<null>";
        }

        public override void FillSchema(Schema schema)
        {
        }
    }
}
