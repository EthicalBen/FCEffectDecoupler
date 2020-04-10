using BS_Utils.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace FCEffectDecoupler {
    public static partial class Decoupler {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812:Avoid uninstantiated internal classes", Justification = "Called by Unity")]
        private class FCEffectMB:MonoBehaviour {
            private bool comboBreak;
            private int lastNoteId;
            private const float timeoutLength = 5.0f;
            private IEnumerator<WaitForSeconds> coroutine;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Called by Unity")]
            private void Start() {
                BSEvents.levelRestarted += Hide;
                BSEvents.levelQuit += Hide;
                BSEvents.comboDidBreak += ComboBreak;
            }
            internal void ReInit(int lastNoteId) {
                this.lastNoteId = lastNoteId;
                BSEvents.noteWasCut -= LastNoteCheck;
                BSEvents.noteWasCut += LastNoteCheck;
            }
            private void LastNoteCheck(NoteData arg1, NoteCutInfo arg2, int arg3) {
                if(arg1.id == lastNoteId && arg2.allIsOK && !comboBreak) {
                    FCEffect.SetActive(true);
                    coroutine = FallbackHide(timeoutLength);
                    StartCoroutine(coroutine);
                }
            }
            private static IEnumerator<WaitForSeconds> FallbackHide(float timeoutLength) {
                yield return new WaitForSeconds(timeoutLength);
                FCEffect.SetActive(false);
            }
            private void ComboBreak() {
                comboBreak = true;
            }
            internal void Hide(StandardLevelScenesTransitionSetupDataSO arg1, LevelCompletionResults arg2) {
                Hide();
            }
            internal void Hide() {
                comboBreak = false;
                FCEffect.SetActive(false);
                StopCoroutine(coroutine);
            }
            private void Update() {
                if(Input.GetKeyDown(KeyCode.KeypadDivide)) {
                    FCEffect.SetActive(!FCEffect.activeSelf);
                }
            }
        }
    }
}