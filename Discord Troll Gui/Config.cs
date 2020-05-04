using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Troll_Gui
{
    public class Config
    {
        public string path;

        public Config(string inpath)
        {
            this.path = inpath;
        }
        /*
        Details
        State
        SmallImageText
        LargeImageKey
        LargeImageText
        SmallImageKey
        */
        private bool Fileexists()
        {
            bool ret = File.Exists(path);
            return ret;
        }
        private void Createfile()
        {
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine("Discord Feel Config");
            }
        }
        public void FileInit()
        {
            if (!Fileexists())
                Createfile();
        }
        public void Addentry(string key, string value)
        {
            if (Contains(key))
            {
                Editval(key, value);
            }
            else
            {
                string line = key;
                line += "=";
                line += value;
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine(line);
                }
            }
        }
        public string Getvalue(string key)
        {
            string value = null;
            string line;
            bool working = true;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null && working)
                {
                    string[] lineelements = line.Split('=');
                    if (lineelements[0] == key)
                    {
                        value = lineelements[1];
                        working = false;
                    }
                }
            }
            return value;
        }
        public void Editval(string key, string newval)
        {
            //Einen neuen Wert dem Key "key" zuweisen.
            string line;
            string oldline = "";
            bool working = true;

            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null && working)
                {
                    if (line.Contains(key))
                    {
                        oldline = line;
                        working = false;
                    }
                }
            }

            string newline;
            newline = key;
            newline += "=";
            newline += newval;


            string text = File.ReadAllText(path);
            text = text.Replace(oldline, newline);
            File.WriteAllText(path, text);
        }
        public bool Contains(string key)
        {
            bool ret = false;
            string line = null;
            bool working = true;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null && working)
                {
                    if (line.Contains(key))
                    {
                        ret = true;
                        working = false;
                    }
                }
            }
            return ret;
        }
        public void DelVal(string key)
        {
            string line;
            string oldline = "";
            bool working = true;

            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null && working)
                {
                    if (line.Contains(key))
                    {
                        oldline = line;
                        working = false;
                    }
                }
            }
            string text = File.ReadAllText(path);
            text = text.Replace(oldline, "");
            File.WriteAllText(path, text);

            var tempFile = Path.GetTempFileName();
            var linesToKeep = File.ReadLines(path).Where(l => l != "");

            File.WriteAllLines(tempFile, linesToKeep);

            File.Delete(path);
            File.Move(tempFile, path);
        }
    }
}
