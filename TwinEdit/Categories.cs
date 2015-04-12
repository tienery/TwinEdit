using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TwinEdit
{
    public partial class Categories : Form
    {
        private CategoryInfo current;
        private int selectedIndex;

        public Categories()
        {
            InitializeComponent();

            PopulateList();
        }

        private void PopulateList()
        {
            lbCategories.Items.Clear();
            foreach (CategoryInfo c in Globals.Categories)
            {
                lbCategories.Items.Add("" + c.Id + ": " + c.Title);
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void lbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(lbCategories.SelectedIndex > -1)) return;
            if (current != null)
            {
                if (Regex.IsMatch(txtId.Text, @"\d+"))
                {
                    foreach (CategoryInfo c in Globals.Categories)
                        if (c.Id == Convert.ToInt32(txtId.Text) && c != current)
                        {
                            MessageBox.Show("This ID already exists with another category.");
                            return;
                        }

                    current.Id = Convert.ToInt32(txtId.Text);
                }
                current.Title = txtTitle.Text;
                current.Tags = txtTags.Text;
            }

            lbCategories.SelectedIndexChanged -= lbCategories_SelectedIndexChanged;
            selectedIndex = lbCategories.SelectedIndex;

            PopulateList();

            if (selectedIndex > -1)
                lbCategories.SelectedIndex = selectedIndex;
            lbCategories.SelectedIndexChanged += lbCategories_SelectedIndexChanged;

            current = Globals.Categories[selectedIndex];

            txtId.Text = "" + current.Id;
            txtTitle.Text = current.Title;
            txtTags.Text = current.Tags;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Globals.Categories.Add(new CategoryInfo() { Id = lbCategories.Items.Count });
            PopulateList();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (current == null)
            {
                MessageBox.Show("A category has not been selected.");
                return;
            }
            var response = MessageBox.Show("Are you sure you wish to remove" + current.Title + "?", "", MessageBoxButtons.YesNo);
            if (response == System.Windows.Forms.DialogResult.Yes)
            {
                Globals.Categories.Remove(current);
            }
        }

    }
}
