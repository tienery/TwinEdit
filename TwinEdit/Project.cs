using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Helpers;
using System.IO;

namespace TwinEdit
{
    public class Project
    {

        public ProjectInfo project;
        
        public Project()
        {
            project = new ProjectInfo();
            project.Categories = new List<CategoryInfo>();

            Globals.Project = this;
        }

        public bool Save()
        {
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfd.Filter = "JSON Files|*.json";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                project.Categories = Globals.Categories;
                File.WriteAllText(sfd.FileName, Json.Encode(project));

                //Generate Tweecode
                var twee = sfd.FileName.Substring(0, sfd.FileName.LastIndexOf('.')) + ".twee";
                var content = "";

                foreach (CategoryInfo c in project.Categories)
                {
                    foreach (PassageInfo p in c.Passages)
                    {
                        if (p.Title == "Start")
                        {
                            content += ":: " + p.Title;
                            if (p.Tags != "")
                                content += " [" + p.Tags + "]\n";
                            content += p.Content + "\n";
                            break;
                        }
                    }
                    break;
                } 
                
                foreach (CategoryInfo c in project.Categories)
                {
                    foreach (PassageInfo p in c.Passages)
                    {
                        if (p.Title != "Start")
                        {
                            content += ":: " + p.Title;
                            if (p.Tags != "")
                                content += " [" + p.Tags + "]\n";
                            content += p.Content + "\n";
                        }
                    }
                }

                content += ":: StoryTitle\n" + project.Title + "\n";
                content += ":: StoryAuthor\n" + project.Author + "\n";

                File.WriteAllText(twee, content);

                Globals.JustSavedPath = twee;

                return true;
            }
            return false;
        }

        public static void Load(string file)
        {
            if (Globals.Project != null)
            {
                var response = MessageBox.Show("A project already exists. Do you wish to save the current one first?", "Warning", MessageBoxButtons.YesNoCancel);

                if (response == DialogResult.Yes)
                    if (!Globals.Project.Save())
                        return;
            }
            var p = new Project();
            Globals.Project.project = Json.Decode<ProjectInfo>(File.ReadAllText(file));
            if (Globals.Categories == null)
                Globals.Categories.AddRange(Globals.Project.project.Categories.AsEnumerable());
            else
            {
                Globals.Categories.Clear();
                Globals.Categories.AddRange(Globals.Project.project.Categories.AsEnumerable());
            }
        }

    }
}
