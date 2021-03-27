using JsonViewer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;

namespace JsonViewer.Model
{
    public class NodeViewOption
    {
        public int SimpleCount { get; set; } = 3;
        public bool ShowGlobalPoint { get; set; } = true;

        public HashSet<string> RetainedColumns 
        {
            get
            {
                if (retainedColumns == null)
                    retainedColumns = new HashSet<string> { "key" };
                return retainedColumns;
            }

            set => retainedColumns = value; 
        }

        public HashSet<string> HideColumns
        {
            get
            {
                if (hideColumns == null)
                    hideColumns = new HashSet<string> { "buildings[].key" };
                return hideColumns;
            }

            set => hideColumns = value;
        }


        private HashSet<string> retainedColumns;
        private HashSet<string> hideColumns;
    }
}
