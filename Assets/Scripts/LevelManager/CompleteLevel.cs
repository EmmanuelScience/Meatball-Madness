using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class CompleteLevel : MonoBehaviour
{

    // Reference to level manager
    private LevelManager levelManager;
    // Reference to level number
    private int levelNumber;

    // Reference to final cutscene for victory
    public PlayableDirector playableDirector;

    public GameObject frogAI;
    public GameObject wayPointMarker;

    void Start()
    {
        // Check if page object is attached to this game object
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        if (levelManager == null) {
            throw new Exception("Make sure there is a Level Manager in the Scene");
        }

        // Get the level number by getting the scene which is in the format: "Level" + levelNumber
        String sceneName = SceneManager.GetActiveScene().name;
        levelNumber = (int) Char.GetNumericValue(sceneName[5]);
    }

    // When the player reaches the end of the level
    void OnTriggerEnter(Collider other) { 
        if (other.tag == "Player") {
            if (levelNumber >= levelManager.GetCurrentLevel()) {
                levelManager.SetCurrentLevel(levelNumber + 1);
            }
            other.gameObject.SetActive(false);
            Audio.AudioController.instance.gameObject.SetActive(false);
            frogAI.SetActive(false);
            wayPointMarker.SetActive(false);
            playableDirector.gameObject.SetActive(true);
        }
    }

    public void WarpToHubWorld() {
        if (levelManager.GetCurrentLevel() == levelManager.amountOfLevels + 1) {
            levelManager.GoToWinScreen();
        } else {
            levelManager.ChangeSceneToHubWorld();
        }
    }
}
