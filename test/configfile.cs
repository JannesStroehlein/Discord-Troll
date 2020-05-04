using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class configfile
    {
        /*
        Zeit
        Program
        Klasse
        Typ
        Nachricht
        */

        public static string dirpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string filename = "\\LogViewer Config.txt";

        private static bool Fileexists()
        {
            bool ret = File.Exists(dirpath + filename);
            return ret;
        }

        private static void Createfile()
        {
            using (StreamWriter w = File.AppendText(dirpath + filename))
            {
                w.WriteLine("LogViewer Config " + config.date());
            }
        }

        public static void FileInit()
        {
            if (!Fileexists())
                Createfile();
        }

        public static void Addentry(string key, string value)
        {
            string line = key;
            line += "=";
            line += value;
            using (StreamWriter w = File.AppendText(dirpath + filename))
            {
                w.WriteLine(line);
            }
        }

        public static string Getvalue(string key)
        {
            string value = null;
            string line;
            bool working = true;
            using (StreamReader sr = new StreamReader(dirpath + filename))
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

        public static void Editval(string key, string newval)
        {
            //Einen neuen Wert dem Key "key" zuweisen.
            string line;
            string oldline = "";
            bool working = true;

            using (StreamReader sr = new StreamReader(dirpath + filename))
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


            string text = File.ReadAllText(dirpath + filename);
            text = text.Replace(oldline, newline);
            File.WriteAllText(dirpath + filename, text);
        }

        public static bool Contains(string key)
        {
            bool ret = false;
            string line = null;
            bool working = true;
            using (StreamReader sr = new StreamReader(dirpath + filename))
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

        public static void DelVal(string key)
        {
            string line;
            string oldline = "";
            bool working = true;

            using (StreamReader sr = new StreamReader(dirpath + filename))
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
            string text = File.ReadAllText(dirpath + filename);
            text = text.Replace(oldline, "");
            File.WriteAllText(dirpath + filename, text);

            var tempFile = Path.GetTempFileName();
            var linesToKeep = File.ReadLines(dirpath + filename).Where(l => l != "");

            File.WriteAllLines(tempFile, linesToKeep);

            File.Delete(dirpath + filename);
            File.Move(tempFile, dirpath + filename);
        }
    }
}
