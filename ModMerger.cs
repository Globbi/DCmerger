using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DC_Mod_Merger
{
    public partial class MainWindow : Form
    {
        private ModManager manager;
        private bool badExit;
        private string warnings;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.HOME = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (!Program.HasSettings)
            {
                if (!RequestLibrary())
                {
                    ErrorMessage("This Steam library doesn't contain Dead Cells.", "Oops");
                    return;
                }
                if (!RequestTool())
                {
                    ErrorMessage("This is not the PAKTool.exe", "Oops");
                    return;
                }
                Program.WriteSettings();
            }
            else
            {
                Program.ReadSettings();
            }

            if (!Program.CheckForModMerger())
            {
                ErrorMessage("Please download \"Mod Merger\" mod from the Workshop", "No Mod");
                return;
            }
            
            manager = new ModManager();
            RefreshLists();
            UpdateWarnings();
        }
        
        private bool RequestLibrary()
        {
            if (!Program.DefaultSteamLibrary())
            {
                MessageBox.Show("Can't find Dead Cells on default path.\n" +
                    "Please select the Steam library that contains Dead Cells.\n\n" +
                    "(Usually it's just the Steam folder unless you have a\n" +
                    "separate Library for games.)", "Setup");
                if (!Program.RequestSteamFolder()) return false;
                Program.TOOL_PATH = Program.STEAM + Program.TOOL_PATH;
            }
            return true;
        }

        private bool RequestTool()
        {
            if (!Program.DefaultTool())
            {
                MessageBox.Show("Unable to locate PAKTool.\n" +
                    "Please select PAKTool.exe manually.", "Setup");
                if (!Program.RequestPAKTool()) return false;
            }
            return true;
        }

        //Active List
        private void ListUsed_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.listActive.SelectedItem == null) return;
            this.listActive.DoDragDrop(this.listActive.SelectedItem, DragDropEffects.Move);
        }

        private void ListUsed_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void ListUsed_DragDrop(object sender, DragEventArgs e)
        {
            //Magic
            Point point = listActive.PointToClient(new Point(e.X, e.Y));
            int index = listActive.IndexFromPoint(point);
            if (index < 0) index = listActive.Items.Count - 1;
            
            //Reorder
            ModEntry mod = e.Data.GetData(typeof(ModEntry)) as ModEntry;
            manager.Reorder(listActive.Items.IndexOf(mod), index);
            listActive.Items.Remove(mod);
            listActive.Items.Insert(index, mod);
            UpdateWarnings();
        }


        //Menu Strip
        private void RefreshModListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.RefreshMods();
            UpdateWarnings();
            RefreshLists();
        }

        private void PAKToolPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Program.RequestPAKTool())
                Program.WriteSettings();
            else
                MessageBox.Show("I don't think this is the thing I'm looking for", "Oops");
        }

        private void LibraryPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.RequestSteamFolder())
                Program.WriteSettings();
            else
                MessageBox.Show("This Library doesn't have Dead Cells", "Oops");
        }

        private void CreditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Made by Glob ¯\_(ツ)_/¯" + "\nHope you enjoy!", "Credits");
        }


        //Buttons
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if(listPassive.SelectedIndex >= 0)
            {
                manager.ActivateMod(listPassive.SelectedIndex);
                RefreshLists();
                UpdateWarnings();
            }
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            if (listActive.SelectedIndex >= 0)
            {
                manager.DeactivateMod(listActive.SelectedIndex);
                RefreshLists();
                UpdateWarnings();
            }
        }

        private void ButtonBuild_Click(object sender, EventArgs e)
        {
            manager.AssambleMod();
            MessageBox.Show("Done.");
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            manager.ResetLists();
            UpdateWarnings();
            RefreshLists();
        }

        private void LabelWarning_Click(object sender, EventArgs e)
        {
            MessageBox.Show(warnings, "Overrides and Scripts");
        }


        //Additional
        private void UpdateWarnings()
        {
            warnings = manager.GetWarnings();
            if(warnings != "")
            {
                pictureWarning.Visible = true;
            }
            else
            {
                pictureWarning.Visible = false;
            }
        }

        private void RefreshLists()
        {
            listPassive.Items.Clear();
            foreach (var entry in manager.PassiveMods)
            {
                listPassive.Items.Add(entry);
            }
            listActive.Items.Clear();
            foreach (var entry in manager.ActiveMods)
            {
                listActive.Items.Add(entry);
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!badExit) manager.Cereal();
        }

        private void ErrorMessage(string text, string titel)
        {
            MessageBox.Show(text, titel);
            badExit = true;
            Application.Exit();
        }
    }
}
