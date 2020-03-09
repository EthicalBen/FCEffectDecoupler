using BS_Utils.Utilities;
using IPA;
using UnityEngine.SceneManagement;
using FCEffectDecoupler.HarmonyPatches;

namespace FCEffectDecoupler {

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812:Avoid unistantiated internal classes", Justification = "Instantiated by BSIPA")]
    internal class Plugin:IBeatSaberPlugin {
        static bool runOnce;
        public void OnApplicationStart() {
            BSEvents.OnLoad();
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
        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene) { }
        public void OnUpdate() { }
        public void OnFixedUpdate() { }
        public void OnApplicationQuit() { }
    }
}
