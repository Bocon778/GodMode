using HarmonyLib;
using UnityEngine;

namespace GodMode.Patches
{
    [HarmonyPatch(typeof(PlayerHealth))]
    class GodModePatch
    {
        private static bool godModeEnabled = false;

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void StartPatch(PlayerHealth __instance)
        {
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

                if (BigMessageUI.instance != null)
                {
                    string message = "GOD MODE " + (godModeEnabled ? "ENABLED" : "DISABLED");
                    string emoji = godModeEnabled ? "+" : "-";
                    Color mainColor = godModeEnabled ? new Color(0.5f, 1f, 0f) : new Color(1f, 0.3f, 0.3f);
                    Color flashColor = godModeEnabled ? Color.white : Color.black;
                    
                    BigMessageUI.instance.BigMessage(message, emoji, 42f, mainColor, flashColor);
                    Traverse.Create(BigMessageUI.instance).Field("bigMessageTimer").SetValue(2f);
                }
            }
        }
    }

    [HarmonyPatch(typeof(BigMessageUI))]
    class BigMessageUIPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        private static bool UpdatePrefix(BigMessageUI __instance, ref float ___bigMessageTimer)
        {
            if (___bigMessageTimer > 0f)
            {
                ___bigMessageTimer -= Time.deltaTime * 0.5f;
                return false;
            }
            return true;
        }
    }
}
