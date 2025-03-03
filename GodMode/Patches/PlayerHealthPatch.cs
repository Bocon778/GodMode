using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace GodMode.Patches
{
    [HarmonyPatch(typeof(PlayerHealth))]
    class GodModePatch
    {
        private static bool godModeEnabled = false; // Start with false

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void StartPatch(PlayerHealth __instance)
        {
            // Set initial state when player spawns
            Traverse.Create(__instance).Field("godMode").SetValue(godModeEnabled);
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        private static void UpdatePatch(PlayerHealth __instance)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.G))
            {
                godModeEnabled = !godModeEnabled;
                Traverse.Create(__instance).Field("godMode").SetValue(godModeEnabled);
                Debug.Log("God Mode: " + (godModeEnabled ? "Enabled" : "Disabled"));
            }
        }
    }
}
