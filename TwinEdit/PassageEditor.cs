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
    public partial class PassageEditor : Form
    {
        private PassageInfo _info;
        public PassageInfo info { get { return _info; } private set { _info = value; Title = _info.Title; Tags = _info.Tags; Content = _info.Content; } }

        public string Title { get { return txtTitle.Text; } set { txtTitle.Text = value; } }
        public string Tags { get { return txtTags.Text; } set { txtTags.Text = value; } }
        public int Category { get { return cmbCategories.SelectedIndex; } set { cmbCategories.SelectedIndex = value; previousCategory = value; } }
        public string Content { get { return codeEdit1.Text; } set { codeEdit1.Text = value; } }
        private int previousCategory;

        public PassageEditor(PassageInfo info, int category)
        {
            InitializeComponent();

            this.info = info;

            foreach (CategoryInfo c in Globals.Categories)
                cmbCategories.Items.Add("" + c.Id + ": " + c.Title);

            Category = category;

            codeEdit1.Language = FastColoredTextBoxNS.Language.HTML;
        }

        private void rdbPassage_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPassage.Checked)
                codeEdit1.Language = FastColoredTextBoxNS.Language.HTML;
        }

        private void rdbJavaScript_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbJavaScript.Checked)
                codeEdit1.Language = FastColoredTextBoxNS.Language.JS;
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (previousCategory != cmbCategories.SelectedIndex)
                ChangeCategory(previousCategory, cmbCategories.SelectedIndex);
        }

        private void ChangeCategory(int from, int to)
        {
            CategoryInfo c;
            if (Globals.Categories[from].Passages.IndexOf(info) > -1)
            {
                (c = Globals.Categories[to]).Passages.Add(info);
                var previousInfo = info;
                info = c.Passages[c.Passages.Count - 1];
                Globals.Categories[from].Passages.Remove(previousInfo);
            }
        }

        private void PassageEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SearchExisting(Title))
            {
                MessageBox.Show("A passage with this title already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            if (Regex.IsMatch(Content, @"\[\[((\w+\s*)+?)\|((\w+\s*)+?)\]\]") && rdbPassage.Checked && (!txtTags.Text.Contains("script") || !txtTags.Text.Contains("style")))
            {
                var nonexistPassages = new List<PassageInfo>();
                var match = Regex.Matches(Content, @"\[\[((\w+\s*)+?)\|((\w+\s*)+?)\]\]");
                foreach (Match m in match)
                {
                    var value = m.Groups[3].Value;
                    if (!SearchExisting(value))
                        nonexistPassages.Add(new PassageInfo() { Title = value, Tags = info.Tags });
                }

                if (nonexistPassages.Count > 0)
                {
                    var passages = "";
                    foreach (PassageInfo p in nonexistPassages)
                        passages += nonexistPassages.IndexOf(p) == nonexistPassages.Count - 1 ? p.Title : p.Title + ", ";
                    var response = MessageBox.Show("You have made links to passages that do not exist:\n " + passages + "\n\nWould you like to add them to the current category now?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (response == System.Windows.Forms.DialogResult.Yes)
                        Globals.Categories[Category].Passages.AddRange(nonexistPassages.AsEnumerable());
                }
            }


            info.Title = Title;
            info.Tags = Tags;
            info.Content = Content;
        }

        private bool SearchExisting(string title)
        {
            foreach (CategoryInfo c in Globals.Categories)
            {
                foreach (PassageInfo p in c.Passages)
                {
                    if (p.Title == title && p != info)
                        return true;
                }
            }
            return false;
        }
    }
}
