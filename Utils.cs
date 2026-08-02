using System.Linq;
using Exiled.Events.Commands.Reload;
using PlayerRoles;

namespace AnnounceLogPlugin
{
    public class Utils
    {
        public static string GetFactionColor(Faction faction)
        {
            return faction switch
            {
                Faction.SCP => "red",
                Faction.FoundationStaff => "blue",
                Faction.FoundationEnemy => "green",
                Faction.Flamingos => "pink",
                _ => "gray",
            };
        }

        public static bool HasGun(Exiled.API.Features.Player player)
        {
            return player.Items.Any(item =>
                item.Type == ItemType.GunCOM15 ||
                item.Type == ItemType.GunCOM18 ||
                item.Type == ItemType.GunFSP9 ||
                item.Type == ItemType.GunCrossvec ||
                item.Type == ItemType.GunLogicer ||
                item.Type == ItemType.GunShotgun ||
                item.Type == ItemType.GunRevolver ||
                item.Type == ItemType.GunAK ||
                item.Type == ItemType.GunE11SR ||
                item.Type == ItemType.ParticleDisruptor ||
                item.Type == ItemType.GunSCP127);
        }

        public static bool IsPlayerOnFoundationSide(Exiled.API.Features.Player Player)
        {
            if (Player.IsNTF || 
            Player.Role == RoleTypeId.Scientist || 
            Player.Role == RoleTypeId.FacilityGuard)
            {
                return true;
            }

            return false;
        }

        public static bool IsPlayerOnCISide(Exiled.API.Features.Player Player)
        {
            if (Player.IsCHI || 
            Player.Role == RoleTypeId.ClassD)
            {
                return true;
            }

            return false;
        }

        public static bool IsPlayerOnSCPSide(Exiled.API.Features.Player Player)
        {
            if (Player.IsScp)
            {
                return true;
            }

            return false;
        }
    }
}