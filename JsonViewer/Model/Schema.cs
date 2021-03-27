using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonViewer.Model
{
    public class Schema
    {
        public Schema Parent { get; set; }
        public string Name { get; set; }
        public NodeType NodeType { get; set; }
        public List<Schema> SubSchemas { get; } = new List<Schema>();

        public override string ToString()
        {
            return $"{Name}, {NodeType.ToString()}";
        }

        public bool IsSimple(NodeViewOption nodeViewOption)
        {
            return SubSchemas.Count <= nodeViewOption.SimpleCount
                && SubSchemas.All(s => s.NodeType == NodeType.Value);
        }

        public List<Schema> GetArrayAncestors()
        {
            var ancestors = new List<Schema>();
            var current = this;
            while (current != null)
            {
                if (current.NodeType == NodeType.Array)
                    ancestors.Add(current);
                current = current.Parent;
            }
            return ancestors;
        }

        public List<string> GetPath()
        {
            var path = new LinkedList<string>();
            var current = this;
            while (current != null)
            {
                path.AddFirst(current.Name);
                current = current.Parent;
            }
            return path.ToList();
        }

        public string GetDisplayName()
        {
            var names = new List<string>();
            var current = this;
            while (current != null)
            {
                names.Add(current.NodeType == NodeType.Array 
                    ? current.Name + "[]" : current.Name);
                current = current.Parent;
            }

            var displayName = "";
            for (var i = 0; i < names.Count; ++i)
            {
                if (i == 0) displayName = names[i];
                else displayName = names[i] + "." + displayName;
            }
            return displayName;
        }

        public List<NodeColumn> GetColumns(NodeViewOption nodeViewOption)
        {
            LinkedList<NodeColumn> allColumns;
            if ((NodeType == NodeType.Object || NodeType == NodeType.Array) && IsSimple(nodeViewOption))
            {
                allColumns = new LinkedList<NodeColumn>();
                allColumns.AddLast(new NodeColumn
                {
                    Name = GetDisplayName(),
                    NodeType = NodeType,
                    Path = Node.ToOne(GetPath()),
                });
            }
            else
            {
                allColumns = new LinkedList<NodeColumn>(GetColumnsForCurrentLevel(true, nodeViewOption));
            }

            var currentSchema = Parent;
            while (currentSchema != null)
            {
                var currentColumns = currentSchema.GetColumnsForCurrentLevel(false, nodeViewOption);
                for (var i = currentColumns.Count - 1; i >= 0; --i)
                    allColumns.AddFirst(currentColumns[i]);

                currentSchema = currentSchema.Parent;
            }

            return allColumns.ToList();
        }

        private List<NodeColumn> GetColumnsForCurrentLevel(bool firstLevel, NodeViewOption nodeViewOption)
        {
            var columns = new List<NodeColumn>();

            foreach (var subSchema in SubSchemas
                .Where(s => s.NodeType != NodeType.Object 
                    && s.NodeType != NodeType.Array
                    || s.NodeType != NodeType.Array && s.IsSimple(nodeViewOption)))
            {
                if (!firstLevel && !nodeViewOption.RetainedColumns.Contains(subSchema.Name))
                {
                    continue;
                }
                columns.Add(new NodeColumn
                {
                    Name = subSchema.GetDisplayName(),
                    NodeType = subSchema.NodeType,
                    Path = Node.ToOne(subSchema.GetPath()),
                });
            }

            return columns;
        }
    }
}
