using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;

namespace AnnounceLogPlugin
{
    public static class ServerSettings
    {
        public static TwoButtonsSetting? ShowLogsSetting { get; private set; }
        private static Translation? Translation => Plugin.Instance?.Translation;

        public static void RegisterSettings()
        {
            ShowLogsSetting = new(
                id: 2176,
                label: Translation?.ShowLogs ?? string.Empty,
                firstOption: Translation?.Yes ?? string.Empty,
                secondOption: Translation?.No ?? string.Empty,
                defaultIsSecond: false,
                hintDescription: Translation?.ShowLogsDescription ?? string.Empty,
                onChanged: (player, setting) =>
                {
                    var showLogs = (setting as TwoButtonsSetting)?.IsFirst ?? true;
                    player.SessionVariables["ShowLogs"] = showLogs;
                }
            );

            SettingBase.Register(new[] { ShowLogsSetting });
        }

        public static void UnregisterSettings()
        {
            if (ShowLogsSetting != null)
                SettingBase.Unregister(settings: new[] { ShowLogsSetting });
        }


        public static bool ShouldShowLogs(Player player) => !(player.SessionVariables.TryGetValue("ShowLogs", out var value) && value is bool enabled && !enabled);
    };
}