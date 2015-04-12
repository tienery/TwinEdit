using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Web.Helpers;

namespace TwinEdit
{
    public partial class MainForm : Form
    {
        private Categories categories;
        private bool inSearch;

        public MainForm()
        {
            InitializeComponent();

            if (File.Exists("settings.json"))
                Globals.Settings = Json.Decode<Settings>(File.ReadAllText("settings.json"));
            else
                Globals.Settings = new Settings();
        }

        private void btnModifyCategories_Click(object sender, EventArgs e)
        {
            if (Globals.Project == null)
            {
                MessageBox.Show("There is no active project to modify.");
                return;
            }
            categories = new Categories();
            categories.FormClosed += categories_FormClosed;
            categories.ShowDialog();
        }

        void categories_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopulateCategories();
        }

        private void PopulateCategories()
        {
            cmbCategories.Items.Clear();
            foreach (CategoryInfo c in Globals.Categories)
                cmbCategories.Items.Add("" + c.Id + ": " + c.Title);
        }

        private void btnAddPassage_Click(object sender, EventArgs e)
        {
            if (Globals.Project == null)
            {
                MessageBox.Show("There is no active project to modify.");
                return;
            }

            if (cmbCategories.SelectedIndex > -1)
            {
                var c = Globals.Categories[cmbCategories.SelectedIndex];
                c.Passages.Add(new PassageInfo() { Tags = c.Tags });
                PopulatePassages();
            }
        }

        private void PopulatePassages()
        {
            if (cmbCategories.SelectedIndex > -1 && !inSearch)
            {
                pnlContent.Controls.Clear();
                var c = Globals.Categories[cmbCategories.SelectedIndex];
                var tempList = c.Passages.AsEnumerable().Reverse();
                foreach (PassageInfo p in tempList)
                {
                    var infoUi = new PassageInfoUI(p, cmbCategories.SelectedIndex);
                    infoUi.OnPassageDeleted += infoUi_OnPassageDeleted;
                    infoUi.OnRefreshInformation += infoUi_OnRefreshInformation;
                    pnlContent.Controls.Add(infoUi);
                }
            }
        }

        void infoUi_OnRefreshInformation()
        {
            PopulatePassages();
        }

        void infoUi_OnPassageDeleted()
        {
            PopulatePassages();
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePassages();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.Project == null)
                new Project();
            else {
                var response = MessageBox.Show("A project already exists. Do you wish to save the current one first?", "Warning", MessageBoxButtons.YesNoCancel);

                if (response == DialogResult.Yes)
                    if (!Globals.Project.Save())
                        return;
            }
            new AddEditProject().ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "JSON Files|*.json";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Project.Load(ofd.FileName);

            PopulateCategories();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.Project != null)
                Globals.Project.Save();
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.Project != null)
                new AddEditProject().ShowDialog();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (Globals.Project == null)
            {
                MessageBox.Show("There is no active project to search.");
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                inSearch = true;
                var passagesToAdd = new List<PassageInfo>();
                foreach (PassageInfo p in Globals.Project.project.Categories[cmbCategories.SelectedIndex].Passages)
                {
                    if (cmbSearchType.SelectedIndex == 0)
                    {
                        if (p.Title.Contains(txtSearch.Text))
                            passagesToAdd.Add(p);
                    }
                    else if (cmbSearchType.SelectedIndex == 1)
                    {
                        var tags = p.Tags.Split(' ');
                        foreach (string val in tags)
                        {
                            if (txtSearch.Text.Contains(val))
                            {
                                passagesToAdd.Add(p);
                                break;
                            }
                        }
                    }
                    else if (cmbSearchType.SelectedIndex == 2)
                    {
                        if (p.Content.Contains(txtSearch.Text))
                            passagesToAdd.Add(p);
                    }
                }

                pnlContent.Controls.Clear();
                passagesToAdd.Reverse();

                foreach (var p in passagesToAdd)
                {
                    pnlContent.Controls.Add(new PassageInfoUI(p, cmbCategories.SelectedIndex));
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                txtSearch.Text = "";
                inSearch = false;
                PopulatePassages();
            }
        }

        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.Project == null)
            {
                MessageBox.Show("There is no active project to build.");
                return;
            }

            if (Globals.Settings.TweeSourceDirectory == "")
            {
                MessageBox.Show("The Twee source code directory has not been set. Please locate the source code of the Twee engine.");
                return;
            }
            if (!Directory.Exists(Globals.Settings.TweeSourceDirectory))
            {
                MessageBox.Show("The Twee source code directory selected does not exist.");
                return;
            }

            if (Globals.Project.Save())
            {
                var batch = "python \"" + Globals.Settings.TweeSourceDirectory + "\\twee\" ";
                batch += jonahToolStripMenuItem.Checked ? "" : "-t sugarcane ";
                
                var sfd = new SaveFileDialog();
                sfd.Filter = "HTML File|*.htm";
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    batch += "\"" + Globals.JustSavedPath + "\" > \"" + sfd.FileName + "\"";
                else
                    return;

                File.WriteAllText("build.bat", batch);
                var response = MessageBox.Show("A 'build.bat' file has been generated in this applications directory. Would you like to go there now?", "Confirm", MessageBoxButtons.YesNo);

                if (response == System.Windows.Forms.DialogResult.Yes)
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory);

                try
                {
                    //var cmdInfo = new ProcessStartInfo();
                    //cmdInfo.UseShellExecute = false;
                    //cmdInfo.Arguments = "/c " + batch;
                    //cmdInfo.FileName = "cmd.exe";
                    //var cmd = Process.Start(cmdInfo);

                    //do
                    //{
                    //    if (cmd.HasExited)
                    //    {
                    //        if (cmd.ExitCode < 0 || cmd.ExitCode > 0)
                    //        {
                    //            MessageBox.Show("The project did not build successfully due to errors.", 
                    //                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            var response = MessageBox.Show("The project was built successfully. Would you like to view it now?", "Success", MessageBoxButtons.YesNo);

                    //            if (response == System.Windows.Forms.DialogResult.Yes)
                    //                Process.Start(sfd.FileName);
                    //            return;
                    //        }
                    //    }
                    //} while (cmd.WaitForExit(500));

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error ocurred while attempting to build the project:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Globals.Project != null)
            {
                var response = MessageBox.Show("Save before exiting?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (response == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!Globals.Project.Save())
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else if (response == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            File.WriteAllText("settings.json", Json.Encode(Globals.Settings));
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsUI().ShowDialog();
        }

        private void jonahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sugarcaneToolStripMenuItem.Checked = false;
        }

        private void sugarcaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            jonahToolStripMenuItem.Checked = false;
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cmbSearchType.SelectedIndex = 0;
        }
    }
}
