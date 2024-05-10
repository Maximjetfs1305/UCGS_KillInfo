using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using Rocket.Unturned.Events;
using SDG.Unturned;
using UnityEngine;
using System.Runtime.CompilerServices;
using System;
using Steamworks;

namespace UCGS.KillInfo
{
    public static class Extensions
    {
        public static SteamPlayer SteamPlayer(this Player player)
        {
            foreach (SteamPlayer steamPlayer in Provider.clients)
            {
                if (player.channel.owner.playerID.steamID == steamPlayer.playerID.steamID)
                {
                    return steamPlayer;
                }
            }
            return null;
        }
        public static SteamPlayer SteamPlayer(this CSteamID cSteamID)
        {
            foreach (SteamPlayer steamPlayer in Provider.clients)
            {
                if (cSteamID == steamPlayer.playerID.steamID)
                {
                    return steamPlayer;
                }
            }
            return null;
        }
        public static void ChatPrintAllPlayers(string message)
        {
            foreach (var client in Provider.clients)
            {
                client.ChatPrint(message);
            }
        }
        public static void ChatPrint(this SteamPlayer steamPlayer, string message, SteamPlayer fromPlayer = null)
        {
            ChatManager.serverSendMessage((Plugin.Config.ChatPrefix) ? "<color=#ffffff>[</color><color=#00ffff>KillInfo</color><color=#ffffff>]</color> " + message : message, Color.white, fromPlayer, steamPlayer, EChatMode.SAY, Plugin.Config.ChatIcon, true);
        }
        public static void WriteConsole(string message) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("KillInfo");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
