using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Profiel Config
             * Name=Watching u.
             * DispName=Watching You
             * ClientID=606441822264360964
             * RPAssets=eye_blak, eye_white
            */
            Config loadprofile = new Config(@"C:\Users\Jannes\Documents\.discord tolls\.profile\jannes.txt");
            loadprofile.FileInit();
            Console.WriteLine("Name : " + loadprofile.Getvalue("Name"));
            Console.WriteLine("DispName : " + loadprofile.Getvalue("DispName"));
            Console.WriteLine("ClientID : " + loadprofile.Getvalue("ClientID"));
            Console.WriteLine("Assets : ");
            string[] assets = loadprofile.Getvalue("RPAssets").Split(',');
            for (int i = 0; i < assets.Length; i++)
            {
                Console.WriteLine("    - " + assets[i]);
            }
            Console.ReadKey();
        }
    }
}
