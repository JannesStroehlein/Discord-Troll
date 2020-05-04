using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord.Commands;
using DiscordRPC;
using DiscordRPC.Logging;

namespace Discord_Troll
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicExample();
        }
        static void BasicExample()
        {
            // == Create the client
            var client = new DiscordRpcClient("606102103232086036")
            {
                Logger = new DiscordRPC.Logging.ConsoleLogger()
            };
            // == Subscribe to some events
            client.OnReady += (sender, msg) =>
            {
                //Create some events so we know things are happening
                Console.WriteLine("Connected to discord with user {0}", msg.User.Username);
            };

            client.OnPresenceUpdate += (sender, msg) =>
            {
                //The presence has updated
                Console.WriteLine("Presence has been updated! ");
            };
            // == Initialize
            client.Initialize();

            // == Set the presence
            Console.WriteLine(client.ApplicationID);
            client.SetPresence(new RichPresence()
            {
                Details = "Abgefuckt",
                State = "Diese scheiß API...",
                Assets = new Assets()
                {
                    SmallImageText = "",
                    LargeImageKey = "janneslogofinish",
                    LargeImageText = "Was glotzt du so?",
                    SmallImageKey = "gl0_-_dead"
                }
            });
            Console.ReadKey();

            // == At the very end we need to dispose of it
            client.Dispose();
        }
    }
}
