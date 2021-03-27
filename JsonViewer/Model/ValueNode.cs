using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonViewer.Model
{
    public class ValueNode : Node
    {
        public string Value { get; set; }
        public override NodeType NodeType => NodeType.Value;
        public override string DisplayName => Name;

        public override string DisplayValue(NodeViewOption nodeViewOption)
        {
            return Parent != null && Parent.NodeType == NodeType.Array
                ? (Parent as ObjectNode).GetString(nodeViewOption) : Value;
        }

        public override void FillSchema(Schema schema)
        {
            if (schema.SubSchemas.Any(s => s.Name == Name)) return;
            schema.SubSchemas.Add(new Schema
            {
                Parent = schema,
                Name = Name,
                NodeType = NodeType,
            });
        }
    }
}
