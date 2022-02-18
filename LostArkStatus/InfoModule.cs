using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Discord;

namespace LostArkStatus
{
    using Discord.Commands;

    // Keep in mind your module **must** be public and inherit ModuleBase.
    // If it isn't, it will not be discovered by AddModulesAsync!
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        // ~say hello world -> hello world
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
            => ReplyAsync(echo);
        
        // ~status trixion -> Trixion is busy
        [Command("status")]
        [Summary("return the status of the designated server")]
        public Task StatusAsync([Remainder] [Summary("Name of the server")] string srvName)
        {
            var servers = ServerFetcher.GetServerList();
            
            var server = servers.FirstOrDefault(s => s.Name.ToLower().Equals(srvName, StringComparison.Ordinal));

            return ReplyAsync(server is null ? "Server not found" : $"{server.Name} is {server.Status}");
        }
            
		
        // ReplyAsync is a method on ModuleBase 
    }
}