using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using GodMode.Patches;

namespace GodMode
{
    [BepInPlugin(modGUID, modeName, modVersion)]
    public class GodMode : BaseUnityPlugin
    {
        private const string modGUID = "Bocon.GodMode";
        private const string modeName = "God Mode";
        private const string modVersion = "1.3.0";

        private const string ASCII_LOGO = @"
                _____           _  ___  ___          _        _                     _          _        
               |  __ \         | | |  \/  |         | |      | |                   | |        | |      _   
               | |  \/ ___   __| | | .  . | ___   __| | ___  | |     ___   __ _  __| | ___  __| |    _| |_    
               | | __ / _ \ / _` | | |\/| |/ _ \ / _` |/ _ \ | |    / _ \ / _` |/ _` |/ _ \/ _` |   |_   _|
               | |_\ \ (_) | (_| | | |  | | (_) | (_| |  __/ | |___| (_) | (_| | (_| |  __/ (_| |     |_|
                \____/\___/ \__,_| \_|  |_/\___/ \__,_|\___| \_____/\___/ \__,_|\__,_|\___|\__,_|
";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static GodMode Instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo(ASCII_LOGO);

            harmony.PatchAll(typeof(GodMode));
            harmony.PatchAll(typeof(GodModePatch));
        }
    }
}
