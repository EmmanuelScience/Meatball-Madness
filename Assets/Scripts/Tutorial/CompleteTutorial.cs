using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class CompleteTutorial : MonoBehaviour
{

    // Reference to final cutscene for victory
    public PlayableDirector playableDirector;
    public GameObject frog;

    // When the player reaches the end of the level
    void OnTriggerEnter(Collider other) { 
        if (other.tag == "Player") {
            other.gameObject.SetActive(false);
            Audio.AudioController.instance.gameObject.SetActive(false);
            frog.SetActive(false);
            playableDirector.gameObject.SetActive(true);
        }
    }

    public void WarpToHubWorld() {
        SceneManager.LoadScene("HubWorld");
    }
}
