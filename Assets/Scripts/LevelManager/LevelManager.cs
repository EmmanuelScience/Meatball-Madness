using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

// Keeps track of all the levels in the game
// Controls logic for switching scenes

public class LevelManager : MonoBehaviour
{

    // Use Singelton Design Pattern
    public static LevelManager instance;

    // How many levels are in the scene
    public int amountOfLevels;

    // The level the player is currently on. The player can access the current level number and any previous level
    [SerializeField]
    private int currentLevel = 1;

    // If the player is on a level portal, this is the number of the level for that level portal
    private int portalLevelNumber;

    void Awake() {
        //Let the gameobject persist over the scenes
        DontDestroyOnLoad(gameObject);
        //Check if the control instance is null
        if (instance == null)
        {
            //This instance becomes the single instance available
            instance = this;
        }
        //Otherwise check if the control instance is not this one
        else if (instance != this)
        {
            //In case there is a different instance destroy this one.
            Destroy(gameObject);
        }
    }


    // Makes sure that all levels can be loaded
    // IMPORTANT: Name each scene representing each level with the Format "Level" + levelnumber.
    //            For example, the scene for the first level needs to be named "Level1"
    void Start()
    {
        for (int i = 1; i <= amountOfLevels; i++) {
            if (!Application.CanStreamedLevelBeLoaded("Level" + i)) {
                throw new Exception("The Scene: Level" + i + " doesn't exist. Make sure to add this scene if you want a Level" + i);
            }
        }

        if (!Application.CanStreamedLevelBeLoaded("HubWorld")) {
            throw new Exception("Make sure a scene called HubWorld exists!");
        }

        ActivateStartLevelButton();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Runs everytime the scene is loaded
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        ActivateStartLevelButton();
    }

    void ActivateStartLevelButton() {
        // Activate the Start Level Button if the player is on the Hubworld
        if (SceneManager.GetActiveScene().name == "HubWorld") {
            GameObject startButton = GameObject.Find("/UIManager/LevelPortalPopUp/Background/CanEnterLevel/StartLevelButton");
            startButton.GetComponent<Button>().onClick.AddListener(ChangeSceneToPortalLevelNumber);
            GameObject levelPortalPopUp = GameObject.Find("/UIManager/LevelPortalPopUp");
            levelPortalPopUp.SetActive(false);
        }
        Debug.Log("Hello");
    }

    // Checks if the player can enter level
    public bool CanPlayerEnterLevel(int level) {
        if (currentLevel >= level) {
            return true;
        } else {
            return false;
        }
    }

    // Changes the scene to hubworld
    public void ChangeSceneToHubWorld() {
        SceneManager.LoadScene("HubWorld");
    }

    // Update the current level if the player beats a level for the first time

    // Changes the scene to a specfied level
    public void ChangeToLevel(int level) {
        if (level <= 0) {
            throw new Exception("You can't change to a level that is less than or equal to zero.");
        } else if (level > amountOfLevels) {
            throw new Exception("Can't change to Level" + level + ". No level exists past Level" + amountOfLevels + ".");
        } else if (currentLevel < level) {
            throw new Exception("Can't enter Level" + level + ". The player can only enter levels less than or equal to Level" + currentLevel + ".");
        } else {
            SceneManager.LoadScene("Level" + level);
        }
    }

    public void ChangeSceneToPortalLevelNumber() {
        SceneManager.LoadScene("Level" + portalLevelNumber);
    }

    // Set the current level
    public void SetCurrentLevel(int newLevel) {
        currentLevel = newLevel;
    }

    // Get the current level
    public int GetCurrentLevel() {
        return currentLevel;
    }

    // Set the portalLevelNumber Variable
    public void SetPortalLevelNumber(int levelNum) {
        portalLevelNumber = levelNum;
    }

    public void GoToWinScreen() {
        SceneManager.LoadScene("WinScreen");
    }
}
