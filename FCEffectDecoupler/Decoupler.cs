using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FCEffectDecoupler {
    public static partial class Decoupler {
        private static GameObject FCEffect;
        private static Scene? FCScene;
        private static FCEffectMB instance;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Fuck off")]
        internal static void OnLoad() {
            GameObject prefab = CustomSaber.Utilities.SaberAssetLoader.CustomSabers.First(x => x.FileName == "Plasma_Katana.saber").Sabers.transform.Find("FullComboEffect").gameObject;
            FCEffect = UnityEngine.Object.Instantiate(prefab.transform.Find("FullComboRoot").gameObject);
            UnityEngine.Object.Destroy(prefab);
            FCEffect.transform.position = new Vector3(0, 1.5f, 0);
            FCEffect.GetComponentInChildren<AudioSource>().volume = 0.5f;
            FCScene = SceneManager.CreateScene("FCScene");
            SceneManager.MoveGameObjectToScene(FCEffect, FCScene.Value);
            instance = new GameObject("FCEffectMB").AddComponent<FCEffectMB>();
            SceneManager.MoveGameObjectToScene(instance.gameObject, FCScene.Value);
        }
        internal static void ReInitFCEffectMB(int lastNoteId, BeatmapObjectSpawnController beatmapObjectSpawnController) {
            instance.ReInit(lastNoteId, beatmapObjectSpawnController);
        }
        internal static void HideEffect() {
            instance.Hide();
        }
    }
}