namespace AnnounceLogPlugin
{
    using Exiled.API.Interfaces;

    public class Translation : ITranslation
    {
        // Class names
        public string None { get; set; } = "NONE";
        public string Scp173 { get; set; } = "SCP-173";
        public string ClassD { get; set; } = "CLASS-D";
        public string Spectator { get; set; } = "SPECTATOR";
        public string Scp106 { get; set; } = "SCP-106";
        public string NtfSpecialist { get; set; } = "NTF SPECIALIST";
        public string Scp049 { get; set; } = "SCP-049";
        public string Scientist { get; set; } = "SCIENTIST";
        public string Scp079 { get; set; } = "SCP-079";
        public string ChaosConscript { get; set; } = "CHAOS CONSCRIPT";
        public string Scp096 { get; set; } = "SCP-096";
        public string Scp0492 { get; set; } = "SCP-049-2";
        public string NtfSergeant { get; set; } = "NTF SERGEANT";
        public string NtfCaptain { get; set; } = "NTF CAPTAIN";
        public string NtfPrivate { get; set; } = "NTF PRIVATE";
        public string Tutorial { get; set; } = "TUTORIAL";
        public string FacilityGuard { get; set; } = "FACILITY GUARD";
        public string Scp939 { get; set; } = "SCP-939";
        public string CustomRole { get; set; } = "CUSTOM ROLE";
        public string ChaosRifleman { get; set; } = "CHAOS RIFLEMAN";
        public string ChaosMarauder { get; set; } = "CHAOS MARAUDER";
        public string ChaosRepressor { get; set; } = "CHAOS REPRESSOR";
        public string Overwatch { get; set; } = "OVERWATCH";
        public string Filmmaker { get; set; } = "FILMMAKER";
        public string Scp3114 { get; set; } = "SCP-3114";
        public string Destroyed { get; set; } = "DESTROYED";
        public string Flamingo { get; set; } = "FLAMINGO";
        public string AlphaFlamingo { get; set; } = "ALPHA FLAMINGO";
        public string ZombieFlamingo { get; set; } = "ZOMBIE FLAMINGO";
        public string NtfFlamingo { get; set; } = "NTF FLAMINGO";
        public string ChaosFlamingo { get; set; } = "CHAOS FLAMINGO";

        public string Foundation { get; set; } = "FOUNDATION";
        public string ChaosInsurgency { get; set; } = "CHAOS INSURGENCY";

        // Announce log strings
        public string Eliminated { get; set; } = "Eliminated";
        public string InfluenceFor { get; set; } = "Influence for";
        public string TimeToRespawn { get; set; } = "Time to respawn";
        public string HasJoinedTheGame { get; set; } = "{0} has joined the game";
        public string HasLeftTheGame { get; set; } = "{0} has left the game";
        public string FoundationInfluenceIncreased { get; set; } = "Foundation's influence increased";
        public string CIInfluenceIncreased { get; set; } = "Chaos Insurgency's influence increased";
        public string Escorted { get; set; } = "Escorted";
    }
}