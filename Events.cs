using PlayerRoles;
using MEC;

namespace AnnounceLogPlugin
{
    public class Events
    {
        public void OnJoined(Exiled.Events.EventArgs.Player.JoinedEventArgs ev)
        {
            if (Plugin.Instance == null)
            {
                return;
            }

            Plugin.PlayerAnnounceLogInfo info = new Plugin.PlayerAnnounceLogInfo();
            info.player = ev.Player;
            info.UpdateTextCoroutineHandle = Timing.RunCoroutine(Plugin.Instance.DisplayLoop(info));
            Plugin.Instance.playerAnnounceLogInfos[ev.Player] = info;
        }

        public void OnVerified(Exiled.Events.EventArgs.Player.VerifiedEventArgs ev)
        {
            if (Plugin.Instance == null)
            {
                return;
            }

            if (!Plugin.Instance.Config.ShowPlayerJoinNotification)
            {
                return;
            }

            Plugin.Instance.BroadcastAnnounceLog(string.Format(Plugin.Instance.Translation.HasJoinedTheGame,
            ev.Player.Nickname));
        }

        public void OnLeft(Exiled.Events.EventArgs.Player.LeftEventArgs ev)
        {
            if (Plugin.Instance == null)
            {
                return;
            }

            Timing.KillCoroutines(Plugin.Instance.playerAnnounceLogInfos[ev.Player].UpdateTextCoroutineHandle);
            Plugin.Instance.playerAnnounceLogInfos.Remove(ev.Player);

            if (Plugin.Instance.Config.ShowPlayerLeaveNotification)
            {
                Plugin.Instance.BroadcastAnnounceLog(string.Format(Plugin.Instance.Translation.HasLeftTheGame,
ev.Player.Nickname));
            }
        }

        public void OnRestartingRound()
        {

        }

        public void OnInfluenceModified(Faction faction, float newValue)
        {
            if (Plugin.Instance == null)
            {
                return;
            }

            if (!Plugin.Instance.Config.ShowInfluenceIncreasedNotification)
            {
                return;
            }

            if (faction == Faction.FoundationStaff)
            {
                Plugin.Instance.BroadcastAnnounceLog(Plugin.Instance.Translation.FoundationInfluenceIncreased);
            }
            else if (faction == Faction.FoundationEnemy)
            {
                Plugin.Instance.BroadcastAnnounceLog(Plugin.Instance.Translation.CIInfluenceIncreased);
            }
        }

        public void OnDying(Exiled.Events.EventArgs.Player.DyingEventArgs ev)
        {
            if (Plugin.Instance == null)
            {
                return;
            }

            if (!Plugin.Instance.Config.ShowKillNotification)
            {
                return;
            }

            if (ev.Attacker == null)
            {
                return;
            }

            string killedRole = "";
            killedRole = ev.Player.Role.Type switch
            {
                RoleTypeId.None => Plugin.Instance.Translation.None,
                RoleTypeId.Scp173 => Plugin.Instance.Translation.Scp173,
                RoleTypeId.ClassD => Plugin.Instance.Translation.ClassD,
                RoleTypeId.Spectator => Plugin.Instance.Translation.Spectator,
                RoleTypeId.Scp106 => Plugin.Instance.Translation.Scp106,
                RoleTypeId.NtfSpecialist => Plugin.Instance.Translation.NtfSpecialist,
                RoleTypeId.Scp049 => Plugin.Instance.Translation.Scp049,
                RoleTypeId.Scientist => Plugin.Instance.Translation.Scientist,
                RoleTypeId.Scp079 => Plugin.Instance.Translation.Scp079,
                RoleTypeId.ChaosConscript => Plugin.Instance.Translation.ChaosConscript,
                RoleTypeId.Scp096 => Plugin.Instance.Translation.Scp096,
                RoleTypeId.Scp0492 => Plugin.Instance.Translation.Scp0492,
                RoleTypeId.NtfSergeant => Plugin.Instance.Translation.NtfSergeant,
                RoleTypeId.NtfCaptain => Plugin.Instance.Translation.NtfCaptain,
                RoleTypeId.NtfPrivate => Plugin.Instance.Translation.NtfPrivate,
                RoleTypeId.Tutorial => Plugin.Instance.Translation.Tutorial,
                RoleTypeId.FacilityGuard => Plugin.Instance.Translation.FacilityGuard,
                RoleTypeId.Scp939 => Plugin.Instance.Translation.Scp939,
                RoleTypeId.CustomRole => Plugin.Instance.Translation.CustomRole,
                RoleTypeId.ChaosRifleman => Plugin.Instance.Translation.ChaosRifleman,
                RoleTypeId.ChaosMarauder => Plugin.Instance.Translation.ChaosMarauder,
                RoleTypeId.ChaosRepressor => Plugin.Instance.Translation.ChaosRepressor,
                RoleTypeId.Overwatch => Plugin.Instance.Translation.Overwatch,
                RoleTypeId.Filmmaker => Plugin.Instance.Translation.Filmmaker,
                RoleTypeId.Scp3114 => Plugin.Instance.Translation.Scp3114,
                RoleTypeId.Destroyed => Plugin.Instance.Translation.Destroyed,
                RoleTypeId.Flamingo => Plugin.Instance.Translation.Flamingo,
                RoleTypeId.AlphaFlamingo => Plugin.Instance.Translation.AlphaFlamingo,
                RoleTypeId.ZombieFlamingo => Plugin.Instance.Translation.ZombieFlamingo,
                RoleTypeId.NtfFlamingo => Plugin.Instance.Translation.NtfFlamingo,
                RoleTypeId.ChaosFlamingo => Plugin.Instance.Translation.ChaosFlamingo,
                _ => killedRole
            };

            int influenceEarned = 0;
            int timerDecrease = 0;
            if ((Utils.IsPlayerOnFoundationSide(ev.Player) || Utils.IsPlayerOnCISide(ev.Player)) && !ev.Attacker.IsScp && Utils.HasGun(ev.Player))
            {
                timerDecrease = 4;
                influenceEarned = 1;
            }
            else if (ev.Player.IsScp && ev.Player.Role != RoleTypeId.Scp0492)
            {
                timerDecrease = 10;
                influenceEarned = 15;
            }

            string forWhoInfluence = "";
            if ((Utils.IsPlayerOnFoundationSide(ev.Player) || ev.Player.IsScp) && Utils.IsPlayerOnCISide(ev.Attacker))
            {
                forWhoInfluence = Plugin.Instance.Translation.ChaosInsurgency;
            }
            else if ((Utils.IsPlayerOnCISide(ev.Player) || ev.Player.IsScp) && Utils.IsPlayerOnFoundationSide(ev.Attacker))
            {
                forWhoInfluence = Plugin.Instance.Translation.Foundation;
            }

            string message = $"{Plugin.Instance.Translation.Eliminated} [{killedRole}]";
            if (influenceEarned > 0 && forWhoInfluence != "")
            {
                message += $" [+{influenceEarned} {Plugin.Instance.Translation.InfluenceFor} {forWhoInfluence}]";
            }

            if (timerDecrease > 0)
            {
                message += $" [-{timerDecrease} {Plugin.Instance.Translation.TimeToRespawn}]";
            }

            Plugin.Instance.SendAnnounceLog(ev.Attacker, message);
        }

        public void OnEscaping(Exiled.Events.EventArgs.Player.EscapingEventArgs ev)
        {
            if (Plugin.Instance == null)
            {
                return;
            }

            if (!Plugin.Instance.Config.ShowEscortNotification)
            {
                return;
            }

            int timerDecrease = 0;
            if (ev.EscapeScenario == Exiled.API.Enums.EscapeScenario.CuffedClassD)
            {
                timerDecrease = 10;
            }
            else if (ev.EscapeScenario == Exiled.API.Enums.EscapeScenario.CuffedScientist)
            {
                timerDecrease = 20;
            }
            else
            {
                return;
            }

            if (ev.Player.Cuffer == null)
            {
                return;
            }

            string forWhoInfluence = "";
            if (Utils.IsPlayerOnFoundationSide(ev.Player.Cuffer))
            {
                forWhoInfluence = Plugin.Instance.Translation.Foundation;
            }
            else if (Utils.IsPlayerOnCISide(ev.Player.Cuffer))
            {
                forWhoInfluence = Plugin.Instance.Translation.ChaosInsurgency;
            }

            Plugin.Instance.SendAnnounceLog(ev.Player.Cuffer, $"{Plugin.Instance.Translation.Escorted} [{ev.Player.Nickname}] [+5 {Plugin.Instance.Translation.InfluenceFor} {forWhoInfluence}] [-{timerDecrease} {Plugin.Instance.Translation.TimeToRespawn}]");
        }
    }
}