using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonViewer.Model
{
    public class NodeView
    {
        public bool OnlyOne => nodeRows.Count == 1;

        private List<NodeColumn> nodeColumns;
        private List<Dictionary<string, string>> nodeRows;
        private string filteredText = "";

        public NodeView(string filteredText)
        {
            this.filteredText = filteredText;
        }

        public void BuildView(Schema schema, RootNode rootNode, NodeViewOption nodeViewOption)
        {
            nodeColumns = schema.GetColumns(nodeViewOption);

            var nodes = rootNode.GetNodes(schema);
            nodeRows = new List<Dictionary<string, string>>();
            foreach (var node in nodes)
            {
                var nodeRow = new Dictionary<string, string>();
                var firstLevel = true;

                ObjectNode objectNode;
                if (node.NodeType != NodeType.Object
                    && node.NodeType != NodeType.Array)
                {
                    objectNode = node.Parent as ObjectNode;
                }
                else
                {
                    objectNode = node as ObjectNode;
                }

                while (objectNode != null)
                {
                    if (firstLevel && objectNode.NodeType == NodeType.Object 
                        && objectNode.IsSimple(nodeViewOption))
                    {
                        nodeRow[Node.ToOne(objectNode.GetPath())] = objectNode.DisplayValue(nodeViewOption);
                    }
                    else
                    {
                        foreach (var subNode in objectNode.Nodes
                            .Where(n => n.NodeType != NodeType.Array
                                && n.NodeType != NodeType.Object
                                || n.NodeType != NodeType.Array && (n as ObjectNode).IsSimple(nodeViewOption)))
                        {
                            if (!firstLevel && !nodeViewOption.RetainedColumns.Contains(subNode.Name))
                            {
                                continue;
                            }
                            nodeRow[Node.ToOne(subNode.GetPath())] = subNode.DisplayValue(nodeViewOption);
                        }
                    }
                    objectNode = objectNode.Parent as ObjectNode;
                    firstLevel = false;
                }

                nodeRows.Add(nodeRow);
            }
        }

        public List<string> GetColumns(NodeViewOption nodeViewOption)
        {
            var columns = new List<string>();
            if (nodeRows.Count == 1)
            {
                return new List<string> { "<Field>", "<Value>" };
            }
            else
            {
                if (nodeColumns.Count == 0) return columns;
                var maxLevel = nodeColumns.Max(c => c.Name.Count(ch => ch == '.'));
                foreach (var nodeColumn in nodeColumns)
                {
                    var columnName = GetColumnName(nodeColumn);
                    if (!nodeViewOption.HideColumns.Contains(columnName))
                    {
                        columns.Add(columnName);
                    }
                }
            }
            return columns;
        }

        private string GetColumnName(NodeColumn nodeColumn)
        {
            var maxLevel = nodeColumns.Max(c => c.Name.Count(ch => ch == '.'));

            var columnName = "";
            var level = nodeColumn.Name.Count(ch => ch == '.');
            var pos = nodeColumn.Name.LastIndexOf('.');

            if (level < maxLevel)
                pos = nodeColumn.Name.LastIndexOf('.', pos - 1);

            var len = nodeColumn.Name.Length;
            columnName = nodeColumn.Name.Substring(pos + 1, len - pos - 1);

            return columnName;
        }

        private bool IsFiltered(string text)
        {
            return filteredText == "" 
                || filteredText != "" && text.IndexOf(filteredText) != -1;
        }

        public List<List<string>> GetRows(NodeViewOption nodeViewOption)
        {
            var rows = new List<List<string>>();
            if (nodeRows.Count == 1)
            {
                var nodeRow = nodeRows[0];
                foreach (var nodeColumn in nodeColumns)
                {
                    var columnName = GetColumnName(nodeColumn);
                    if (!nodeViewOption.HideColumns.Contains(columnName))
                    {
                        var text = nodeRow.ContainsKey(nodeColumn.Path) ? nodeRow[nodeColumn.Path] : "<null>";
                        if (!IsFiltered(text)) continue;
                        var row = new List<string>
                        {
                            columnName,
                            text,
                        };
                        rows.Add(row);
                    }
                }
            }
            else
            {
                foreach (var nodeRow in nodeRows)
                {
                    var row = new List<string>();
                    foreach (var nodeColumn in nodeColumns)
                    {
                        var columnName = GetColumnName(nodeColumn);
                        if (!nodeViewOption.HideColumns.Contains(columnName))
                        {
                            nodeRow.TryGetValue(nodeColumn.Path, out var value);
                            var text = value ?? "<null>";
                            row.Add(text);
                        }
                    }

                    if (row.Any(t => IsFiltered(t))) rows.Add(row);
                }
            }
            return rows;
        }
    }
}
