using System;
using System.IO;
using System.Windows.Forms;

namespace WinFormsDirectoryTreeControl
{
    public class DirectoryTree : TreeView
    {
        #region Properties

        private char drive;

        public char Drive
        {
            get
            {
                return drive;
            }
            set
            {
                drive = value;
                this.RefreshDisplay();
            }
        }

        #endregion

        #region Events

        public delegate void DirectorySelectedDelegate(object sender, DirectorySelectedEventArgs e);
        public event DirectorySelectedDelegate DirectorySelected;

        #endregion

        #region Overrides

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            base.OnBeforeExpand(e);

            // If a dummy node is found, remove it and read the real directory list.
            if (e.Node.Nodes[0].Text != "*")
            {
                return;
            }

            e.Node.Nodes.Clear();
            this.Fill(e.Node);
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            // Raise the DirectorySelected event.
            this.DirectorySelected?.Invoke(this, new DirectorySelectedEventArgs(e.Node.FullPath));
        }

        #endregion

        #region Methods

        public void RefreshDisplay()
        {
            this.SuspendLayout();

            // Erase the existing tree.
            this.Nodes.Clear();

            // Set the first node.
            var rootNode = new TreeNode(drive + ":\\");
            this.Nodes.Add(rootNode);

            // Fill the first level and expand it.
            this.Fill(rootNode);
            this.Nodes[0].Expand();

            this.ResumeLayout();
        }

        private void Fill(TreeNode dirNode)
        {
            try
            {
                this.TryFil(dirNode);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void TryFil(TreeNode dirNode)
        {
            var dir = new DirectoryInfo(dirNode.FullPath);

            var nodes = new TreeNode[dir.GetDirectories().Length];
            int i = 0;

            foreach (DirectoryInfo dirItem in dir.GetDirectories())
            {
                var newNode = new TreeNode(dirItem.Name);
                newNode.Nodes.Add("*");
                nodes[i] = newNode;
                i++;
            }

            dirNode.Nodes.AddRange(nodes);
        }

        #endregion
    }
}
