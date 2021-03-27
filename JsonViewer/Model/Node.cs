using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonViewer.Model
{
    public abstract class Node
    {
        public string Name { get; set; }
        public Node Parent { get; set; }

        public abstract NodeType NodeType { get; }
        public abstract string DisplayName { get; }

        public abstract string DisplayValue(NodeViewOption nodeViewOption);

        public abstract void FillSchema(Schema schema);

        public List<string> GetPath()
        {
            var path = new LinkedList<string>();
            var current = this;
            while(current != null)
            {
                if (current.Parent == null
                    || current.Parent != null
                    && current.Parent.NodeType != NodeType.Array)
                {
                    path.AddFirst(current.Name);
                }
                current = current.Parent;
            }
            return path.ToList();
        }

        protected static bool ContainPath(List<string> path, List<string> subPath)
        {
            if (subPath.Count > path.Count) return false;
            for (var i = 0; i < subPath.Count; ++i)
            {
                if (subPath[i] != path[i]) return false;
            }
            return true;
        }

        protected static bool IsSamePath(List<string> path1, List<string> path2)
        {
            if (path1.Count != path2.Count) return false;
            for (var i = 0; i < path1.Count; ++i)
            {
                if (path1[i] != path2[i]) return false;
            }
            return true;
        }

        public static string ToOne(List<string> strings)
        {
            var value = "";
            for (var i = 0; i< strings.Count; ++i)
            {
                if (i == 0)
                    value = strings[i];
                else
                    value += "." + strings[i];
            }
            return value;
        }
    }
}
