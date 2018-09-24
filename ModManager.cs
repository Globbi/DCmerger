using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC_Mod_Merger
{
    class ModManager
    {
        private string TEMP = Program.HOME + "\\Temp";
        private string MOD = Program.HOME + "\\MOD";
        private string MOD_LIST = Program.HOME + "\\ModList";

        private List<ModEntry> passivMods = new List<ModEntry>();
        public List<ModEntry> PassiveMods { get { return passivMods; } }
        private ActiveModList activeMods;
        public List<ModEntry> ActiveMods { get { return activeMods.GetMods(); } }

        public ModManager()
        {
            FetchMods();
        }
        
        //Public Functions
        public void FetchMods()
        {
            //Create temp lists
            List<ModEntry> tempPassive = new List<ModEntry>();
            List<ModEntry> tempActive = new List<ModEntry>();

            //Load existing mod list
            if (File.Exists(MOD_LIST)) passivMods = Tools.Deserilize(MOD_LIST);

            //Read mod folder for changes
            bool needToCereal = false;
            foreach (string modPath in Directory.GetDirectories(Program.MOD_PATH))
            {
                if (!modPath.Equals(Program.MOD_MERGER)) //Ignore Mod Merger
                {
                    ModEntry mod = SearchLoadedMods(modPath);
                    if (mod == null) //Such mod has not been loaded yet
                    {
                        needToCereal = true;
                        tempPassive.Add(ModEntry.BuildModEntry(modPath));
                    }
                    else //I know a mod like that
                    {
                        mod.FetchInfo();
                        if (mod.IsActive)
                            tempActive.Add(mod);
                        else
                            tempPassive.Add(mod);

                        passivMods.Remove(mod);
                    }
                }
            }
            if (passivMods.Count > 0) needToCereal = true; //Too many mods?

            passivMods = tempPassive;
            passivMods.Sort();

            activeMods = new ActiveModList(tempActive);

            if (needToCereal) Cereal();
        }

        public void RefreshMods()
        {
            passivMods = new List<ModEntry>();
            activeMods = new ActiveModList();
            FetchMods();
        }

        public void Cereal()
        {
            List<ModEntry> temp = new List<ModEntry>(passivMods);
            temp.AddRange(activeMods.GetMods());
            Tools.Serialize(temp, MOD_LIST);
        }

        public void ActivateMod(int index)
        {
            ModEntry mod = passivMods[index];
            passivMods.RemoveAt(index);
            activeMods.Add(mod);
        }

        public void DeactivateMod(int index)
        {
            ModEntry mod = activeMods.RemoveAt(index);
            InsertSorted(mod);
        }

        public void Reorder(int from, int to)
        {
            activeMods.Move(from, to);
        }

        public void ResetLists()
        {
            foreach(ModEntry mod in ActiveMods)
            {
                mod.IsActive = false;
                InsertSorted(mod);
            }
            activeMods = new ActiveModList();
        }

        public void AssambleMod()
        {
            //Clean up just in case
            if (Directory.Exists(TEMP))
                Directory.Delete(TEMP, true);

            string scriptPath = "";
            for (int i = activeMods.Count - 1; i >= 0; i--)
            {
                //Check for scripts in mod.
                if (activeMods[i].HasStruct)
                    scriptPath = Program.MOD_PATH + "\\" + activeMods[i].ID + "\\Scripts";

                //Unpack mods that need to be merged
                Tools.UnpackResPak(activeMods[i].ID, TEMP); 
            }

            //Do I need to make a res.pak? otherwise just wipe the old one
            if (Directory.EnumerateFileSystemEntries(TEMP).Any())
                Tools.PackResPak(TEMP);
            else
                if (File.Exists(Program.MOD_MERGER + "\\res.pak")) File.Delete(Program.MOD_MERGER + "\\res.pak");

            //Is a mod using a scipts? otherwise just wipe the old scripts folder
            if (scriptPath != "")
            {
                //Copy all directories
                foreach (string dirPath in Directory.GetDirectories(scriptPath, "*", SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(scriptPath, Program.MOD_MERGER + "\\Scripts"));
                //Copy all files
                foreach (string newPath in Directory.GetFiles(scriptPath, "*.*", SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(scriptPath, Program.MOD_MERGER + "\\Scripts"), true);
            }
            else
            {
                //Wipe Scripts folder if no script is used.
                if (Directory.Exists(Program.MOD_MERGER + "\\Scripts"))
                    Directory.Delete(Program.MOD_MERGER + "\\Scripts", true);
            }
            
            //Clean up
            if (Directory.Exists(TEMP)) Directory.Delete(TEMP, true);
        }

        public string GetWarnings()
        {
            string sum = "";
            foreach (string warning in activeMods.GetWarnings())
            {
                sum += warning;
            }
            return sum;
        }
        

        //Private Help Functions
        private void InsertSorted(ModEntry mod)
        {
            var i = passivMods.BinarySearch(mod);
            if (i < 0) i = ~i;
            passivMods.Insert(i, mod);
        }

        private ModEntry SearchLoadedMods(string path)
        {
            return passivMods.Find(e => { return e.Path.Equals(path); });
        }
    }
}