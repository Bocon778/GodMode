using HarmonyLib;
using UnityEngine;
using System.Collections;

namespace GodMode.Patches
{
    [HarmonyPatch(typeof(PlayerHealth))]
    class GodModePatch
    {
        private static bool godModeEnabled = false;
        private static Coroutine messageCoroutine;

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
            bool isChatActive = false;
            if (ChatManager.instance != null)
            {
                isChatActive = (bool)Traverse.Create(ChatManager.instance)
                    .Field("chatActive")
                    .GetValue();
            }

            if (!isChatActive && UnityEngine.Input.GetKeyDown(KeyCode.G))
            {
                godModeEnabled = !godModeEnabled;
                Traverse.Create(__instance).Field("godMode").SetValue(godModeEnabled);

                if (BigMessageUI.instance != null)
                {
                    string message = "GOD MODE " + (godModeEnabled ? "ENABLED" : "DISABLED");
                    string emoji = godModeEnabled ? "+" : "-";
                    Color mainColor = godModeEnabled ? new Color(0.5f, 1f, 0f) : new Color(1f, 0.3f, 0.3f);
                    Color flashColor = godModeEnabled ? Color.white : Color.black;

                    if (messageCoroutine != null)
                    {
                        __instance.StopCoroutine(messageCoroutine);
                    }

                    BigMessageUI.instance.BigMessage(message, emoji, 42f, mainColor, flashColor);

                    messageCoroutine = __instance.StartCoroutine(HideMessageAfterDelay(2f));
                }

                Debug.Log("God Mode: " + (godModeEnabled ? "Enabled" : "Disabled"));
            }
        }

        private static IEnumerator HideMessageAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            if (BigMessageUI.instance != null)
            {
                BigMessageUI.instance.Hide();
            }
        }
    }
}

