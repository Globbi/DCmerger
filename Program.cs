using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DC_Mod_Merger
{
    static class Program
    {
        public static string HOME;
        public static string STEAM = @"C:\Program Files\Steam";
        public static string MOD_PATH { get { return STEAM + @"\steamapps\workshop\content\588650"; } }
        public static string TOOL_PATH = @"\steamapps\common\Dead Cells\ModTools\PAKTool.exe";
        public static string MOD_MERGER { get { return MOD_PATH + @"\1518057942"; } }
        private static string SETTINGS = "settings";

        private static string SettingsPath { get { return HOME + "\\" + SETTINGS; } }
        public static bool HasSettings { get { return File.Exists(SettingsPath); } }
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        internal static bool RequestSteamFolder()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog()
            {
                Description = "Please select.",
                ShowNewFolderButton = false
            };

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                STEAM = fbd.SelectedPath;
                if(Directory.Exists(MOD_PATH)) return true;
                return false;
            }
            else
            {
                return false;
            }
        }

        internal static bool RequestPAKTool()
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                AddExtension = true,
                InitialDirectory = STEAM,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                TOOL_PATH = ofd.FileName;
                return true;
            }
            else
            {
                return false;
            }
        }
        
        internal static bool ReadSettings()
        {
            string[] paths = File.ReadAllLines(SettingsPath);
            if (paths.Length != 2)
            {
                return false;
            }
            STEAM = paths[0];
            TOOL_PATH = paths[1];
            return true;
        }

        internal static void WriteSettings()
        {
            string paths = STEAM + Environment.NewLine + TOOL_PATH;
            File.WriteAllText(SettingsPath, paths);
        }

        internal static bool DefaultSteamLibrary()
        {
            if(!Directory.Exists(MOD_PATH))
            {
                STEAM = STEAM.Replace("Program Files", "Program Files (x86)");
                return Directory.Exists(MOD_PATH);
            }
            return true;
        }

        internal static bool DefaultTool()
        {
            return File.Exists(TOOL_PATH);
        }

        internal static bool CheckForModMerger()
        {
            return Directory.Exists(MOD_MERGER);
        }
    }
}
