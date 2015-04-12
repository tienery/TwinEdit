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
    public partial class AddEditProject : Form
    {
        public AddEditProject()
        {
            InitializeComponent();
        }

        private void AddEditProject_Load(object sender, EventArgs e)
        {
            txtTitle.Text = Globals.Project.project.Title;
            txtAuthor.Text = Globals.Project.project.Author;
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            Globals.Project.project.Title = txtTitle.Text;
        }

        private void txtAuthor_TextChanged(object sender, EventArgs e)
        {
            Globals.Project.project.Author = txtAuthor.Text;
        }
    }
}
