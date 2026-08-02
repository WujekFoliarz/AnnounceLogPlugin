using System;
using CommandSystem;

namespace AnnounceLogPlugin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class BroadcastAnnounceLogCmd : ICommand
    {
        public string Command { get; set; } = "broadcastlog";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = { "blog" };

        /// <inheritdoc />
        public string Description { get; set; } = "Broadcasts announce log to every player.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (Plugin.Instance == null)
            {
                response = "Plugin instance was null!";
                return false;
            }

            if (arguments.Count <= 0)
            {
                response = "No arguments!";
                return false;
            }

            string result = "";

            int index = 0;
            foreach (var argument in arguments)
            {
                bool isLast = index == arguments.Count - 1;
                result += argument;

                if (!isLast)
                {
                    result += " ";
                }

                index++;
            }

            Plugin.Instance.BroadcastAnnounceLog(result);
            response = "Success";
            return true;
        }
    }
}