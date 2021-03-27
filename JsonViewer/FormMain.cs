using JsonViewer.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonViewer
{
    public partial class FormMain : Form
    {
        private string fileName;
        private RootNode rootNode;
        private NodeViewOption nodeViewOption = new NodeViewOption();
        private Timer txtFindTimer;

        public FormMain()
        {
            InitializeComponent();
            InitViewOptions();
            InitControls();
        }

        public FormMain(string fileName)
        {
            InitializeComponent();
            InitViewOptions();
            InitControls();

            this.fileName = fileName;
            OpenFile();
        }

        private string GetOptionFile()
        {
            var strFileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            var nPos = strFileName.LastIndexOf('\\');
            var strPath = strFileName.Substring(0, nPos);
            return strPath + "\\Option.json";
        }

        private void InitViewOptions()
        {
            var optionFile = GetOptionFile();
            if (File.Exists(optionFile))
            {
                nodeViewOption = JsonConvert.DeserializeObject<NodeViewOption>(File.ReadAllText(optionFile));
            }
        }

        private void WriteToOptionFile()
        {
            var optionFile = GetOptionFile();
            File.WriteAllText(optionFile, JsonConvert.SerializeObject(nodeViewOption, Formatting.Indented));
        }

        private void openJsonFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                Filter = "Json files (*.json)|*.json",
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            fileName = openFileDialog.FileName;
            OpenFile();
        }

        private void OpenFile()
        {
            try
            {
                var jsonText = File.ReadAllText(fileName);
                var rootObject = JObject.Parse(jsonText);
                var jsonName = Path.GetFileNameWithoutExtension(fileName);
                rootNode = NodeConverter.ConvertFromJObject(rootObject, jsonName);

                InitialViews();
                Text = $"Json Viewer - {jsonName} ({Path.GetDirectoryName(fileName)})";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                InitialViews();
            }
        }

        private void InitialViews()
        {
            FillTreeView();

            treeView.SelectedNode = treeView.Nodes.Count == 0 
                ? null : treeView.Nodes[0];

            FillGrid();
        }

        private void FillTreeView()
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            treeView.EndUpdate();

            if (rootNode == null) return;

            Cursor = Cursors.WaitCursor;
            treeView.BeginUpdate();
            var treeNode = treeView.Nodes.Add(Path.GetFileNameWithoutExtension(fileName));
            treeNode.ImageIndex = 2;
            treeNode.SelectedImageIndex = treeNode.ImageIndex;
            treeNode.Tag = rootNode.Schema;
            FillTreeView(rootNode.Schema, treeNode);
            treeNode.Expand();
            treeView.EndUpdate();

            Cursor = Cursors.Default;
        }

        private void FillTreeView(Schema schema, TreeNode treeNode)
        {
            foreach (var subSchema in schema.SubSchemas
                .Where(s => s.NodeType == NodeType.Array || s.NodeType == NodeType.Object))
            {
                var subTreeNode = treeNode.Nodes.Add(subSchema.Name);
                subTreeNode.Tag = subSchema;
                subTreeNode.ImageIndex = subSchema.NodeType == NodeType.Object ? 0 : 1;
                subTreeNode.SelectedImageIndex = subTreeNode.ImageIndex;
                FillTreeView(subSchema, subTreeNode);
            }
        }

        private LinkedList<string> GetCurrentPaths()
        {
            var paths = new LinkedList<string>();
            if (treeView.SelectedNode != null)
            {
                var current = treeView.SelectedNode.Tag as Schema;
                while (current != null)
                {
                    paths.AddFirst(Node.ToOne(current.GetPath()));
                    current = current.Parent;
                }
            }
            return paths;
        }
        
        private void SetPath(LinkedList<string> paths)
        {
            if (paths.Count > 0 && treeView.Nodes.Count > 0)
            {
                var treeNodes = treeView.Nodes;
                TreeNode lastNode = null;
                foreach (var path in paths)
                {
                    var found = false;
                    foreach (var treeNode in treeNodes)
                    {
                        var schema = (treeNode as TreeNode).Tag as Schema;
                        if (Node.ToOne(schema.GetPath()) == path)
                        {
                            found = true;
                            lastNode = treeNode as TreeNode;
                            lastNode.Expand();
                            treeNodes = lastNode.Nodes;

                            break;
                        }
                    }
                    if (!found) return;
                }

                treeView.SelectedNode = lastNode;
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileName == null) return;

            var paths = GetCurrentPaths();

            OpenFile();

            SetPath(paths);
        }

        private void InitControls()
        {
            var gridFont = new Font("Calibri", 12);
            var gridPadding = new Padding(5, 1, 5, 1);

            dataGridView.ColumnHeadersDefaultCellStyle.Font = gridFont;
            dataGridView.ColumnHeadersDefaultCellStyle.Padding = gridPadding;

            dataGridView.DefaultCellStyle.Font = gridFont;
            dataGridView.DefaultCellStyle.Padding = gridPadding;

            dataGridView.AlternatingRowsDefaultCellStyle.Font = gridFont;
            dataGridView.AlternatingRowsDefaultCellStyle.Padding = gridPadding;

            treeView.Font = new Font("Calibri", 14);

            optionToolStripMenuItem.Checked = true;

            chkGlobalPoint.Checked = nodeViewOption.ShowGlobalPoint;

            txtFindTimer = new Timer
            {
                Interval = 700
            };
            txtFindTimer.Tick += new EventHandler(textTimer_Tick);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            if (treeView.SelectedNode == null || treeView.SelectedNode.Tag == null)
                return;

            Cursor = Cursors.WaitCursor;

            var filteredText = chkFilter.Checked && txtFind.Text.Trim() != "" ? txtFind.Text : "";

            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();

            var currentSchema = treeView.SelectedNode.Tag as Schema;

            var nodeView = new NodeView(filteredText);
            nodeView.BuildView(currentSchema, rootNode, nodeViewOption);

            var columns = nodeView.GetColumns(nodeViewOption);
            dataGridView.ColumnCount = columns.Count;
            for (var iColumn = 0; iColumn < columns.Count; ++iColumn)
            {
                var column = columns[iColumn];
                dataGridView.Columns[iColumn].Name = column;
            }

            var rows = nodeView.GetRows(nodeViewOption);
            foreach (var row in rows)
            {
                if (row.Count == 0) continue;
                dataGridView.Rows.Add(row.ToArray());
            }

            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            for (var iColumn = 0; iColumn < dataGridView.ColumnCount; ++iColumn)
            {
                var column = dataGridView.Columns[iColumn];
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (!nodeView.OnlyOne)
            {
                var text = treeView.SelectedNode.Text;
                var pos = text.LastIndexOf('[');
                if (pos == -1)
                {
                    text += $" [{dataGridView.RowCount}]";
                    treeView.SelectedNode.Text = text;
                }
            }

            SetGridColor();

            Cursor = Cursors.Default;
        }

        private static string DealString(string s)
        {
            var t = s.Trim();
            return t;
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formOption = new FormOption();
            formOption.txtRetained.Text = string.Join(",", nodeViewOption.RetainedColumns.ToArray());
            formOption.txtHidden.Text = string.Join(",", nodeViewOption.HideColumns.ToArray());

            if (formOption.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var strings = formOption.txtRetained.Text.Split(',').ToList().Select(t => t = DealString(t));
                    nodeViewOption.RetainedColumns = new HashSet<string>(strings);

                    strings = formOption.txtHidden.Text.Split(',').ToList().Select(t => t = DealString(t));
                    nodeViewOption.HideColumns = new HashSet<string>(strings);

                    var paths = GetCurrentPaths();
                    FillTreeView();
                    SetPath(paths);
                    WriteToOptionFile();
                }
                catch(Exception)
                {
                }
            }
        }

        private void chkGlobalPoint_CheckedChanged(object sender, EventArgs e)
        {
            nodeViewOption.ShowGlobalPoint = chkGlobalPoint.Checked;
            var paths = GetCurrentPaths();
            FillTreeView();
            SetPath(paths);
            WriteToOptionFile();
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            txtFindTimer.Start();
        }

        private void SetGridColor()
        {
            for (var iRow = 0; iRow < dataGridView.RowCount; ++iRow)
            {
                var row = dataGridView.Rows[iRow];
                var isRowHighlighted = false;
                for (var iColumn = 0; iColumn < dataGridView.ColumnCount; ++iColumn)
                {
                    var cell = row.Cells[iColumn];
                    var isCellHighlighted = txtFind.Text.Trim() != ""
                        && cell.Value.ToString().IndexOf(txtFind.Text) != -1;
                    if (isCellHighlighted) isRowHighlighted = true;
                    cell.Style.ForeColor = isCellHighlighted ? Color.Blue : Color.Black;
                }
                row.DefaultCellStyle.BackColor = isRowHighlighted ? Color.LightPink : Color.White;
            }
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void textTimer_Tick(Object sender, EventArgs e)
        {
            if (txtFind.Focused)
            {
                if (chkFilter.Checked)
                    FillGrid();
                else
                    SetGridColor();

                txtFindTimer.Stop();
            }
        }
    }
}
