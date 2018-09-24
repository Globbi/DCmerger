using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DC_Mod_Merger
{
    class ActiveModList
    {
        List<ModEntry> activeMods;
        List<string> warnings = new List<string>();
        
        public ActiveModList()
        {
            activeMods = new List<ModEntry>();
        }

        public ActiveModList(List<ModEntry> modList)
        {
            activeMods = modList;
            for(int i = 0; i < activeMods.Count; i++)
                warnings.Add("");
            ActivateFiles(0, activeMods.Count);
        }

        public void Add(ModEntry entry)
        {
            entry.Active = true;

            activeMods.Add(entry);
            warnings.Add("");

            ActivateFile(activeMods.Count-1);
        }
        
        public void Move(int from, int to)
        {
            ModEntry entry = activeMods[from];
            activeMods.RemoveAt(from);
            activeMods.Insert(to, entry);

            if (from < to)
                ActivateFiles(from, to+1);
            else
                ActivateFiles(to, from+1);
        }

        public ModEntry RemoveAt(int index)
        {
            ModEntry entry = activeMods[index];
            entry.Active = false;

            activeMods.RemoveAt(index);
            warnings.RemoveAt(index);

            ActivateFiles(index, activeMods.Count);

            return entry;
        }

        private void ActivateFiles(int from, int to)
        {
            for(int i = from; i < to; i++)
            {
                ActivateFile(i);
            }
        }

        private void ActivateFile(int index)
        {
            //Init warnings
            if (!activeMods[index].HasStruct)
                warnings[index] = "";
            else
                warnings[index] = activeMods[index].Name + " contains lvl script\n";

            //If first mod in list you are done
            if (index == 0) return;
            
            //Collision logger
            bool[] collision = new bool[index];

            //Looping through each file of a mod
            foreach (string file in activeMods[index].Files)
            {
                //Loop though higher priority mods
                for(int i = 0; i < index; i++)
                {
                    if(activeMods[i].Files.Contains(file))
                    {
                        collision[i] = true;
                        break;
                    }
                }
            }

            for(int j = 0; j < index; j++)
            {
                if(collision[j])
                    warnings[index] += activeMods[index].Name + " <=== " + activeMods[j].Name + "\n";
            }
        }

        public ModEntry this[int index] { get { return activeMods[index]; } }

        public int Count { get { return activeMods.Count; } }

        public List<ModEntry> GetMods()
        {
            return activeMods;
        }

        public List<string> GetWarnings()
        {
            return warnings;
        }
    }

    [XmlRoot("ModList")]
    public class ModList
    {
        public List<ModEntry> Mods { get; set; }
    }

    public class ModEntry : IComparable<ModEntry>
    {
        [XmlElement("Active")]
        public bool Active { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("ID")]
        public string ID { get; set; }
        [XmlElement("HasStruct")]
        public bool HasStruct { get; set; }
        [XmlIgnore]
        public List<string> Files { get; set; }

        public bool IsActive { get { return Active; } }

        public string Path { get { return Program.MOD_PATH + "\\" + ID; } }

        public void FetchFiles()
        {
            Files = PakReader.ReadFilesInPAK(Path+"\\res.pak");
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(ModEntry other)
        {
            return Name.CompareTo(other.Name);
        }

        public static ModEntry BuildModEntry(string path)
        {
            string id = (new System.IO.DirectoryInfo(path)).Name;
            string name = Tools.ReadModName(path + "\\settings.json");
            bool hasStruct = System.IO.Directory.Exists(path + @"\Scripts\Struct");
            List<string> files = PakReader.ReadFilesInPAK(path + @"\res.pak");

            return new ModEntry()
            {
                Name = name,
                ID = id,
                HasStruct = hasStruct,
                Files = files,
                Active = false
            };
        }
    }
}
