using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Diagnostics;

namespace DC_Mod_Merger
{
    static class Tools
    {
        public static string ReadModName(string path)
        {
            if (!File.Exists(path)) return "MissingSettingsJSON";
            string[] entry = File.ReadAllLines(path);
            foreach(string e in entry)
            {
                if (e.Contains("name"))
                {
                    string name = e.Remove(0, e.IndexOf(':'));
                    name = name.Remove(0, name.IndexOf('"', 1)+1);
                    name = name.Trim().Split('"')[0];

                    return name;
                }
            }
            throw new Exception("Invalid JSON");
        }

        public static List<ModEntry> Deserilize(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ModList));
            ModList result;
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                result = (ModList)serializer.Deserialize(fileStream);
            } 
            return result.Mods;
        }

        public static void Serialize(List<ModEntry> list, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ModList));

            using (FileStream file = File.Create(path))
            {
                serializer.Serialize(file, new ModList() { Mods = list });
                file.Close();
            }
        }

        public static bool UnpackResPak(string modID, string OutDir)
        {
            string resPakPath = Program.MOD_PATH + "\\" + modID + "\\res.pak";
            if (!File.Exists(resPakPath)) return false;

            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = "-Expand -OutDir \"" + OutDir + "\" -RefPak \"" + resPakPath + '"';
            start.FileName = '"' + Program.TOOL_PATH + '"';
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;

            /*  {
                Arguments = "-Expand -OutDir \"" + OutDir + "\" -RefPak \"" + resPakPath + '"',
                FileName = '"' + Program.TOOL_PATH + '"',
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };  */

            int exitCode = 0;
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }

            return exitCode == 0 ? true : false;
        }

        public static bool PackResPak(string InDir)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = "-Collapse -InDir \"" + InDir + "\" -OutPak \"" + Program.MOD_MERGER + "\\res.pak\"";
            start.FileName = '"' + Program.TOOL_PATH + '"';
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;

            /*  {
                Arguments = "-Collapse -InDir \"" + InDir + "\" -OutPak \"" + Program.MOD_MERGER + "\\res.pak\"",
                FileName = '"' + Program.TOOL_PATH + '"',
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };  */
            int exitCode = 0;
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }

            FixBytes();

            return exitCode == 0 ? true : false;
        }

        //This method corrects the byte the PAKTool messes up atm.
        private static void FixBytes()
        {
            using (var stream = new FileStream(Program.MOD_MERGER + "\\res.pak", FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = 4;
                byte[] bytes = new byte[4];
                stream.Read(bytes, 0, 4);
                if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);

                int correction = BitConverter.ToInt32(bytes, 0) + 8;

                stream.Position = 4;
                for (int i = 0; i < 4; i++)
                {
                    stream.WriteByte((byte)correction);
                    correction >>= 8;
                }
            }
        }
    }

    static class PakReader
    {
        public static List<string> ReadFilesInPAK(string path)
        {
            if (!File.Exists(path)) return new List<string>();

            List<string> files = new List<string>();
            using (FileStream stream = File.OpenRead(path))
            {
                stream.Position = 4;
                int lastPosition = ParseInt(stream) - 5;
                stream.Position = 18; //set initial read position

                while (stream.Position < lastPosition)
                    ParseStructure(files, stream, "");
            }

            return files;
        }

        private static void ParseStructure(List<string> files, FileStream stream, string dir)
        {
            string str = ParseString(stream);

            bool isFolder = stream.ReadByte() == 1 ? true : false;
            if (isFolder)
            {
                int filesInFolder = ParseInt(stream);
                for (int i = 0; i < filesInFolder; i++)
                {
                    ParseStructure(files, stream, dir + "\\" + str);
                }
            }
            else
            {
                files.Add(dir + "\\" + str);
                stream.Position += 12;
            }
        }

        private static string ParseString(FileStream stream)
        {
            //get string length
            int strLength = stream.ReadByte();

            //read string
            byte[] temp = new byte[strLength];
            stream.Read(temp, 0, strLength);
            return Encoding.Default.GetString(temp);
        }

        private static int ParseInt(FileStream stream)
        {
            byte[] temp = new byte[4];
            stream.Read(temp, 0, 4);
            if (!BitConverter.IsLittleEndian) Array.Reverse(temp);
            return BitConverter.ToInt32(temp, 0);
        }
    }
}
