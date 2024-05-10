using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCGS.KillInfo
{
    public class Config : IRocketPluginConfiguration
    {
        public bool ChatPrefix = true;
        public string ChatIcon = "";
        public bool ShowDistance = true;
        public bool ShowZombieMessage = true;
        public bool ShowAnimalMessages = true;
        public bool ShowConsoleKillMessages = false;
        public bool ShowSuicidieMessages = true;
        public void LoadDefaults()
        {
        }
    }
}
