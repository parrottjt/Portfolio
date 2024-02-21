
namespace FrikinCore.Enumeration
{
    /// Enums need to go here
    /// This is where the generic enums for our game will go
    public class GameEnums
    {
        public enum InputCategories
        {
            Play,
            Menu,
            BugReporter
        }

        public enum TypeOfEnemy
        {
            OrangeFish,
            Crab,
            CrabVarient,
            NinjaStarFish,
            MasterNinjaStarFish,
            Octopus,
            OctopusVarient,
            Skellyfish,
            SkellySniper,
            Pufferfish,
            VariantPufferfish,
            Squid,
            SmallSquid,
            Snail,
            SnailSniper,
            Penguin,
            GhostShotty,
            GhostGassy,
            Raptor,
            VariantRaptor,
            ParatrooperShrimp,
            SwordAndBoard,
            MineJelly,
            MineJellyGassyBubble,
            MineJellySpawner,
            PopOutEel,
            Stingray,
            RamboRay,
            Lionfish,
            Turtle,
            Pirate,
            PirateCannonier,
            BuffMuscle,
            BuffMuscleVariant,
            Surgeonfish,
            SurgeonfishVariant,
            Diver,
            MissileSpawner,
            NotAnEnemy
        }

        public enum DropProbability
        {
            NormalChance,
            HighHealthChance,
            HighTeeth,
            NoDrop,
            NoAmmo
        }

        public enum LevelRewards
        {
            LowDamage,
            NoDamage,
            Speedrun,
            Genicide,
            OnlyRed
        }

        public enum PermanentStats
        {
            WeaponDamageAdjust,
            WeaponRangeAdjust,
            MaxAmmoAdjust,
            InvulnarablityFramesAdjust,
            ShadowShotAdjust,
            ReloadCooldownAdjust,
            DropRateAdjust,

            //Break; Above is the upgrades for story: /\; Below is perm savedFloats for progen \/;
            FireTimeAdjust,
            ReceivedDamageAdjust,
            MoveAdjustSpeed,
            DashForceAdjust,
            LinearDragAdjust,
            ProjSpeedAdjust,
            AngularDragAdjust,
            DuckHealAdjust,
        }

        public enum StatModifiers
        {
            Story_WeaponDamageUpgrade,
            Story_WeaponRangeUpgrade,
            Story_MaxAmmoUpgrade,
            Story_IframesUpgrade,
            Story_ShadowShotUpgrade,
            Story_WeaponReloadTimeUpgrade,
            Story_DropRateUpgrade,
            PerfectDodge,
        }

        public enum MiniGames
        {
            HordeMode
        }

        public enum GameCollectables
        {
            Tooth,
            Teeth,
            BronzeTooth,
            BronzeTeeth,
            SilverTooth,
            SilverTeeth,
            GoldTooth,
            GoldTeeth,
            AmmoFull,
            AmmoHalf,
            Duck,
            SuperDuck,
            PHDucter,
        }

        public enum Damage
        {
            None = 0,
            Light = 1,
            Heavy = 2
        }

        public enum WallLayers
        {
            Border,
            EnemyBorder,
            Destructable
        }

        public enum PlayerMovementEffects
        {
            Stun,
            Freeze,
            Slow,
            Sprint,
            Death,
        }
    }
}