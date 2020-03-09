﻿using Harmony;
using System.Reflection;

namespace FCEffectDecoupler.HarmonyPatches {


    /// <summary>
    /// Harmony Patcher, set to auto-detect
    /// </summary>
    internal static class Patcher {


        /// <summary>
        /// Tracks if the Patcher has run or not.
        /// </summary>
        private static bool runOnce;


        /// <summary>
        /// Used to patch the game, applies all patches.
        /// </summary>
        internal static void Patch() {
            if(!runOnce) {
                HarmonyInstance.Create("com.rolopogo.customplatforms").PatchAll(Assembly.GetExecutingAssembly());
                runOnce = true;
            }
        }
    }
}
