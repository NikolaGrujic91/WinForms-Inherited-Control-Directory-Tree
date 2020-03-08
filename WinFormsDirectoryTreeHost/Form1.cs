using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsDirectoryTreeControl;

namespace WinFormsDirectoryTreeHost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var dirTree = new DirectoryTree
            {
                Size = new Size(this.Width - 30, this.Height - 50),
                Location = new Point(5, 5),
                Drive = char.Parse("C"),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom
            };
            this.Controls.Add(dirTree);
        }
    }
}
