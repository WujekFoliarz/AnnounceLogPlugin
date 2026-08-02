using Exiled.API.Interfaces;

namespace AnnounceLogPlugin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public int ExtendedTextIndex { get; set; } = 30;
    }
}