using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodMode.Patches
{
    [HarmonyPatch(typeof(PlayerHealth))]
    class GodModePatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]

        private static void GodModePatcher(ref bool ___godMode)
        {
            ___godMode = true;
        }
    }
}
