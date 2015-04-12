using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwinEdit
{
    public partial class SettingsUI : Form
    {
        public SettingsUI()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var fd = new FolderBrowserDialog();
            fd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fd.ShowNewFolderButton = false;
            fd.ShowDialog();
            txtSourcePath.Text = fd.SelectedPath;
        }

        private void txtSourcePath_TextChanged(object sender, EventArgs e)
        {
            Globals.Settings.TweeSourceDirectory = txtSourcePath.Text;
        }

        private void SettingsUI_Load(object sender, EventArgs e)
        {
            txtSourcePath.Text = Globals.Settings.TweeSourceDirectory;
        }
    }
}
