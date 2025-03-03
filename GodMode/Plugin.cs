using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using GodMode.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodMode
{
    [BepInPlugin(modGUID, modeName, modVersion)]
    public class GodMode : BaseUnityPlugin
    {
        private const string modGUID = "Bocon.GodMode";
        private const string modeName = "God Mode";
        private const string modVersion = "1.1.0";

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

            mls.LogInfo("Infinite Health mod loaded");

            harmony.PatchAll(typeof(GodMode));
            harmony.PatchAll(typeof(GodModePatch));
        }
    }

}
