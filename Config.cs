using Exiled.API.Interfaces;

namespace AnnounceLogPlugin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public int ExtendedTextIndex { get; set; } = 70;
        public string Language { get; set; } = "en";
        public bool ShowInfluenceIncreasedNotification { get; set; } = true;
        public bool ShowKillNotification { get; set; } = true;
        public bool ShowEscortNotification { get; set; } = true;
        public bool ShowPlayerJoinNotification { get; set; } = true;
        public bool ShowPlayerLeaveNotification { get; set; } = true;
    }
}