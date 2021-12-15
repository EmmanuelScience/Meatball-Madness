using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    public bool isStart;
    public bool isReset;
    public bool isQuit;

    // Reference to level manager
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        // Check if page object is attached to this game object
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        if (levelManager == null) {
            throw new Exception("Make sure there is a Level Manager in the Scene");
        }

    }

    void OnMouseUp()
    {
        if (isStart)
        {
            SceneManager.LoadScene("HubWorld");
        } 
        else if(isReset)
        {
            SceneManager.LoadScene("StartScreen");
            levelManager.SetCurrentLevel(1);
        } 
        else if(isQuit)
        {
            Application.Quit();
        }
    }
}
