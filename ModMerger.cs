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
        private ModManager mm;
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
            if (LoadSettings() && Program.CheckForModMerger())
            {
                mm = new ModManager();
                RefreshLists();
                UpdateWarning();
            }
            else if (!Program.CheckForModMerger())
            {
                MessageBox.Show("Please download \"Mod Merger\" mod from the Workshop", "No Mod");
                badExit = true;
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Something went wrong.", "Oof");
                badExit = true;
                Application.Exit();
            }
        }


        //Settings
        private bool LoadSettings()
        {
            if (!Program.HasSettings)
            {
                return RequestSettings();
            }
            else
            {
                return Program.ReadSettings();
            }
        }

        private bool RequestSettings()
        {
            if (!Program.DefaultSteam())
            {
                MessageBox.Show("Steam is not in defaul dir.\n" +
                    "Please select Steam installation.", "Setup");
                if (!Program.RequestSteamFolder()) return false;
                Program.TOOL_PATH = Program.STEAM + Program.TOOL_PATH;
            }
            if (!Program.DefaultTool())
            {
                MessageBox.Show("PAKTool not found.\n" +
                    "Please select PAKTool.exe manually.", "Setup");
                if (!Program.RequestPAKTool()) return false;
            }
            Program.WriteSettings();
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
            mm.Reorder(listActive.Items.IndexOf(mod), index);
            listActive.Items.Remove(mod);
            listActive.Items.Insert(index, mod);
            UpdateWarning();
        }


        //Menu Strip
        private void RefreshModListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mm.FetchMods();
            UpdateWarning();
            RefreshLists();
        }

        private void CleanRebuildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mm.FreshUnpackMods();
            UpdateWarning();
            RefreshLists();
        }

        private void PAKToolPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Program.RequestPAKTool()) Program.WriteSettings();
        }

        private void SteamPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.RequestSteamFolder()) Program.WriteSettings();
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
                mm.ActivateMod(listPassive.SelectedIndex);
                RefreshLists();
                UpdateWarning();
            }
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            if (listActive.SelectedIndex >= 0)
            {
                mm.DeactivateMod(listActive.SelectedIndex);
                RefreshLists();
                UpdateWarning();
            }
        }

        private void ButtonBuild_Click(object sender, EventArgs e)
        {
            mm.AssambleMod();
            MessageBox.Show("Done.");
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            mm.ResetLists();
            UpdateWarning();
            RefreshLists();
        }

        private void LabelWarning_Click(object sender, EventArgs e)
        {
            MessageBox.Show(warnings, "Overrides and Scripts");
        }


        //Additional
        private void UpdateWarning()
        {
            warnings = mm.GetWarnings();
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
            foreach (var entry in mm.PassiveMods)
            {
                listPassive.Items.Add(entry);
            }
            listActive.Items.Clear();
            foreach (var entry in mm.ActiveMods)
            {
                listActive.Items.Add(entry);
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!badExit) mm.Cereal();
        }
    }
}
