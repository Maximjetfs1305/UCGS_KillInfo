using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UCGS.KillInfo
{
    public class Plugin : RocketPlugin<Config>
    {
        public static Plugin Instance;
        public static Config Config;
        string PluginVersion = "1.0.0";
        protected override void Load()
        {
            Instance = this;
            PlayerLife.RocketLegacyOnDeath = (Action<PlayerLife, EDeathCause, ELimb, CSteamID>)Delegate.Combine(PlayerLife.RocketLegacyOnDeath, new Action<PlayerLife, EDeathCause, ELimb, CSteamID>(RocketLegacyOnDeath));
            Config = base.Configuration.Instance;
            Extensions.WriteConsole("Plugin loaded successfully.");
            Extensions.WriteConsole("Plugin version : " + PluginVersion);
            Extensions.WriteConsole("Author :  Maximjetfs");
            Extensions.WriteConsole("github.com/Maximjetfs1305");
        }

        protected override void Unload()
        {
            Instance = null;
            Config = null;
        }
        void RocketLegacyOnDeath(PlayerLife sender, EDeathCause cause, ELimb limb, CSteamID instigator)
        {
            SteamPlayer client = sender.player.SteamPlayer();
            SteamPlayer killer = instigator.SteamPlayer();
            if (cause == EDeathCause.ZOMBIE && Config.ShowZombieMessage)
            {
                Extensions.ChatPrintAllPlayers(string.Format(Translate(cause.ToString()).Replace("ᐸ", "<").Replace("ᐳ", "> "), client.playerID.characterName));
                return;
            }
            else if (cause == EDeathCause.ANIMAL && Config.ShowAnimalMessages)
            {
                Extensions.ChatPrintAllPlayers(string.Format(Translate(cause.ToString()).Replace("ᐸ", "<").Replace("ᐳ", "> "), client.playerID.characterName));
                return;
            }
            else if (cause == EDeathCause.SUICIDE && Config.ShowSuicidieMessages)
            {
                Extensions.ChatPrintAllPlayers(string.Format(Translate(cause.ToString()).Replace("ᐸ", "<").Replace("ᐳ", "> "), client.playerID.characterName));
                return;
            }
            else if (cause == EDeathCause.KILL && Config.ShowConsoleKillMessages)
            {
                Extensions.ChatPrintAllPlayers(string.Format(Translate(cause.ToString()).Replace("ᐸ", "<").Replace("ᐳ", "> "), client.playerID.characterName));
                return;
            }
            if (killer != null)
            {
                if (cause == EDeathCause.GUN)
                {

                    if (limb == ELimb.SKULL)
                    {
                        Extensions.ChatPrintAllPlayers(string.Format(Translate("GunHeadShot").Replace("ᐸ", "<").Replace("ᐳ", "> "), client.playerID.characterName, killer.playerID.characterName, killer.player.equipment.asset.itemName.ToString(), (Config.ShowDistance ? Math.Round((double)Vector3.Distance(client.player.transform.position, killer.player.transform.position)).ToString() : "")));
                        return;
                    }
                    Extensions.ChatPrintAllPlayers(string.Format(Translate("Gun").Replace("ᐸ", "<").Replace("ᐳ", "> "), client.playerID.characterName, killer.playerID.characterName, killer.player.equipment.asset.itemName.ToString(), (Config.ShowDistance ? Math.Round((double)Vector3.Distance(client.player.transform.position, killer.player.transform.position)).ToString() : "")));
                    return;

                }
                if (cause == EDeathCause.MELEE)
                { 
                    Extensions.ChatPrintAllPlayers(string.Format(Translate("Melee").Replace("ᐸ", "<").Replace("ᐳ", "> "), client.playerID.characterName, killer.playerID.characterName, (Config.ShowDistance ? Math.Round((double)Vector3.Distance(client.player.transform.position, killer.player.transform.position)).ToString() : "")));
                    return;
                }
                if (cause == EDeathCause.PUNCH)
                {
                    Extensions.ChatPrintAllPlayers(string.Format(Translate("Punch").Replace("ᐸ", "<").Replace("ᐳ", "> "), client.playerID.characterName, killer.playerID.characterName, (Config.ShowDistance ? Math.Round((double)Vector3.Distance(client.player.transform.position, killer.player.transform.position)).ToString() : "")));
                    return;
                }
                if (cause == EDeathCause.ROADKILL)
                {
                    Extensions.ChatPrintAllPlayers(string.Format(Translate("Punch").Replace("ᐸ", "<").Replace("ᐳ", "> "), client.playerID.characterName, killer.playerID.characterName));
                    return;
                }
            }
            Extensions.ChatPrintAllPlayers(string.Format(Translate(cause.ToString()).Replace("ᐸ", "<").Replace("ᐳ", "> "), client.playerID.characterName));
        }
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "!!!TEMPLATE!!!", "Using a ᐸcolor=#94b74aᐳText hereᐸ/colorᐳ with a colored text" },
            { "ZOMBIE", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ killed by zombie." },
            { "ANIMAL", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ killed by animal." },
            { "SUICIDE", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ suicidie." },
            { "KILL", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ killed by console." },
            { "GunHeadShot", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ killed in the head by ᐸcolor=#94b74aᐳ{1}ᐸ/colorᐳ using a ᐸcolor=#4ab76bᐳ{2}ᐸ/colorᐳ from a distance ᐸcolor=#864ab7ᐳ{3}ᐸ/colorᐳ m!" },
            { "Gun", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ killed by ᐸcolor=#94b74aᐳ{1}ᐸ/colorᐳ using a ᐸcolor=#4ab76bᐳ{2}ᐸ/colorᐳ from a distance ᐸcolor=#864ab7ᐳ{3}ᐸ/colorᐳ m!" },
            { "Melee", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ was meleed by ᐸcolor=#94b74aᐳ{1}ᐸ/colorᐳ from a distance ᐸcolor=#864ab7ᐳ{2}ᐸ/colorᐳ m!" },
            { "Punch", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ was punched by ᐸcolor=#94b74aᐳ{1}ᐸ/colorᐳ from a distance ᐸcolor=#864ab7ᐳ{2}ᐸ/colorᐳ m!" },
            { "RoadKill","ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ shot down by ᐸcolor=#94b74aᐸcolor=#94b74aᐳ{1}ᐸ/colorᐳᐸ/colorᐳ" },
            { "BLEEDING", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ died of blood loss!"},
            { "BONES", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ died of broken bones!"},
            { "FREEZING", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ frozen!}"},
            { "BURNING", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ was killed by fire!}"},
            { "FOOD", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ starved to death" },
            { "WATER","ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ died of thirst!"},
            { "INFECTION", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ died of blood poisoning!"},
            { "BREATH", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ choked!"},
            { "VEHICLE", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ killed by a car!"},
            { "GRENADE", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ exploded!"},
            { "SHRED", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ shreded!"},
            { "LANDMINE", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ landmine!"},
            { "ARENA", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ killed by arena!"},
            { "GRENADE", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ exploded!"},
            { "MISSILE", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ was annihilated by a missile!"},
            { "CHARGE", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ was obliterated by a charge!"},
            { "SPLASH", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ was killed by splash damage!"},
            { "SENTRY", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ was shot down by a sentry!"},
            { "ACID", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ was killed by acid!"},
            { "BOULDER", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ was killed by a gigantic boulder!"},
            { "BURNER", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ was killed by fire!"},
            { "SPIT", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ was killed by spit!"},
            { "SPARK", "ᐸcolor=#94b74aᐳ{0}ᐸ/colorᐳ has been sparked out!"},
        };
    }
}
