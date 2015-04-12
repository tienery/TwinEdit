using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwinEdit
{
    public partial class PassageInfoUI : UserControl
    {
        public string Title { get { return lblTitle.Text; } private set { lblTitle.Text = value; } }
        private PassageInfo info;
        private int category;

        public PassageInfoUI(PassageInfo p, int category)
        {
            InitializeComponent();

            if (p != null)
            {
                info = p;
                Title = p.Title;
                this.category = category;
            }

            Dock = DockStyle.Top;
        }

        private void PassageInfoUI_Click(object sender, EventArgs e)
        {
            foreach (PassageInfoUI info in Parent.Controls)
            {
                if (info.GetType() == typeof(PassageInfoUI))
                    info.BackColor = SystemColors.Control;
            }
            BackColor = Color.Orange;
        }

        private void PassageInfoUI_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEdit.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Globals.Categories[category].Passages.Remove(info);
            if (OnPassageDeleted != null)
                OnPassageDeleted();
        }

        public delegate void PassageDeleted();
        public event PassageDeleted OnPassageDeleted;

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var edit = new PassageEditor(info, category);
            edit.FormClosed += edit_FormClosed;
            edit.ShowDialog();
        }

        void edit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (OnRefreshInformation != null)
                OnRefreshInformation();
        }

        private void PassageInfoUI_Load(object sender, EventArgs e)
        {
            
        }

        public delegate void RefreshInformation();
        public event RefreshInformation OnRefreshInformation;
    }
}
