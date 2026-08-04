namespace AnnounceLogPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Features;
    using Respawning;
    using RueI.API;
    using RueI.API.Elements;
    using MEC;
    using System.Text;
    using RueI.Utils;
    using Exiled.Loader;

    public class Plugin : Plugin<Config, Translation>
    {
        public static Plugin? Instance { get; private set; }

        public override string Name => "AnnounceLogPlugin";
        public override string Author => "Wujek_Foliarz";
        public override Version Version => new Version(1, 0, 1);

        private Events? Events { get; set; }

        public Dictionary<Player, PlayerAnnounceLogInfo> playerAnnounceLogInfos = new();

        public class AnnounceLogEntry
        {
            public string Text;
            public int Index;

            public AnnounceLogEntry(string text)
            {
                Text = text;
                Index = 0;
            }
        }

        public class PlayerAnnounceLogInfo
        {
            public List<AnnounceLogEntry> AnnounceLogQueue = new List<AnnounceLogEntry>();
            public CoroutineHandle UpdateTextCoroutineHandle;

            public Player? player = null;
        }

        public override void OnEnabled()
        {
            Instance = this;
            Events = new();
            FactionInfluenceManager.InfluenceModified += Events.OnInfluenceModified;
            Exiled.Events.Handlers.Server.RestartingRound += Events.OnRestartingRound;
            Exiled.Events.Handlers.Player.Joined += Events.OnJoined;
            Exiled.Events.Handlers.Player.Left += Events.OnLeft;
            Exiled.Events.Handlers.Player.Dying += Events.OnDying;
            Exiled.Events.Handlers.Player.Verified += Events.OnVerified;
            Exiled.Events.Handlers.Player.Escaping += Events.OnEscaping;
        }

        public override void OnDisabled()
        {
            Instance = null;

            if (Events == null)
            {
                return;
            }

            FactionInfluenceManager.InfluenceModified -= Events.OnInfluenceModified;
            Exiled.Events.Handlers.Server.RestartingRound -= Events.OnRestartingRound;
            Exiled.Events.Handlers.Player.Joined -= Events.OnJoined;
            Exiled.Events.Handlers.Player.Left -= Events.OnLeft;
            Exiled.Events.Handlers.Player.Dying -= Events.OnDying;
            Exiled.Events.Handlers.Player.Verified -= Events.OnVerified;
            Exiled.Events.Handlers.Player.Escaping -= Events.OnEscaping;
            Events = null;

            RemoveAllPlayersCouroutine();
        }

        public Func<string> GetPlayerCurrentAnnounceLog(Player player)
        {
            return () =>
            {
                var info = playerAnnounceLogInfos[player];
                StringBuilder sb = new StringBuilder();
                sb.SetAlignment(RueI.Utils.Enums.AlignStyle.Left);
                sb.SetSize(28);

                foreach (var entry in info.AnnounceLogQueue)
                {
                    sb.SetHorizontalPos(-330);
                    sb.Append(entry.Text.Substring(0, Math.Min(entry.Index, entry.Text.Length)) + "<br>");
                }

                sb.CloseAlign();
                sb.CloseSize();

                return sb.ToString();
            };
        }

        public void BroadcastAnnounceLog(string LogText)
        {
            foreach (var pair in playerAnnounceLogInfos)
            {
                PlayerAnnounceLogInfo info = pair.Value;
                info.AnnounceLogQueue.Add(new AnnounceLogEntry(LogText));
            }
        }

        public void SendAnnounceLog(Player Player, string LogText)
        {
            if (playerAnnounceLogInfos.TryGetValue(Player, out PlayerAnnounceLogInfo info))
            {
                info.AnnounceLogQueue.Add(new AnnounceLogEntry(LogText));
            }
            else
            {
                Log.Error($"[SendAnnounceLog] Couldn't find {Player.Nickname}");
            }
        }

        public void RemovePlayerCoroutine(Player Player)
        {
            if (playerAnnounceLogInfos.TryGetValue(Player, out PlayerAnnounceLogInfo info))
            {
                Timing.KillCoroutines(info.UpdateTextCoroutineHandle);
                playerAnnounceLogInfos.Remove(Player);
            }
        }

        public void RemoveAllPlayersCouroutine()
        {
            foreach (var info in playerAnnounceLogInfos)
            {
                Timing.KillCoroutines(info.Value.UpdateTextCoroutineHandle);
                playerAnnounceLogInfos.Remove(info.Key);
            }
        }

        public IEnumerator<float> DisplayLoop(PlayerAnnounceLogInfo info)
        {
            yield return Timing.WaitForSeconds(5f);
            if (info.player is null)
            {
                Log.Error("[DisplayLoop] info.player is null!");
                yield break;
            }

            DynamicElement dynamicElement = new DynamicElement(400, GetPlayerCurrentAnnounceLog(info.player))
            {
                UpdateInterval = TimeSpan.FromMilliseconds(10)
            };

            RueDisplay display = RueDisplay.Get(info.player);
            Tag DisplayTag = new();
            display.Show(DisplayTag, dynamicElement);

            List<string> toRemove = new List<string>();
            while (true)
            {
                if (info.AnnounceLogQueue.Count > 0)
                {
                    var entry = info.AnnounceLogQueue.FirstOrDefault(x => x.Index < x.Text.Length);

                    if (entry != null)
                    {
                        entry.Index++;
                    }

                    if (info.AnnounceLogQueue.TryGet(0, out AnnounceLogEntry element))
                    {
                        if (element.Index >= element.Text.Length + Config.ExtendedTextIndex)
                        {
                            info.AnnounceLogQueue.RemoveAt(0);
                        }
                        else
                        {
                            element.Index++;
                        }
                    }
                }

                yield return Timing.WaitForSeconds(0.08f);
            }
        }
    }
}