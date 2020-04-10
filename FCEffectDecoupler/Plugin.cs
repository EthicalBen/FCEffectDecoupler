using BS_Utils.Utilities;
using IPA;
using UnityEngine.SceneManagement;
using FCEffectDecoupler.HarmonyPatches;

namespace FCEffectDecoupler {


    [Plugin(RuntimeOptions.SingleStartInit)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812:Avoid unistantiated internal classes", Justification = "Instantiated by BSIPA")]
    internal class Plugin {
        static bool runOnce;
        [OnStart]
        public void OnApplicationStart() {
            BSEvents.OnLoad();
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
            if(scene.name == "MenuCore" && !runOnce) {
                runOnce = true;
                Patcher.Patch();
                Decoupler.OnLoad();
            }
        }
        public void OnSceneUnloaded(Scene scene) {
            if(scene.name == "GameplayCore") {
                Decoupler.HideEffect();
            }
        }
    }
}
