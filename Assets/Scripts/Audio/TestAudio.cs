  
using UnityEngine;

namespace Audio {
    public class TestAudio : MonoBehaviour
    {
    public Audio.AudioController audioController;

#region Unity Functions
#if UNITY_EDITOR
    private void Update() {
        if (Input.GetKeyUp(KeyCode.T)) {
            audioController.PlayAudio(AudioType.Player_Jump, true, 1.0f);
        }
        if (Input.GetKeyUp(KeyCode.G)) {
           // audioController.StopAudio(AudioType, true, 0.0f);
        }
        if (Input.GetKeyUp(KeyCode.B)) {
            //audioController.RestartAudio(AudioType);
        }
    }

#endif
#endregion

    }
}