using System;

namespace WinFormsDirectoryTreeControl
{
    public class DirectorySelectedEventArgs : EventArgs
    {
        public string DirectoryName;

        public DirectorySelectedEventArgs(string directoryName)
        {
            this.DirectoryName = directoryName;
        }
    }
}
