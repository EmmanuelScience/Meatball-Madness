using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelPortal : MonoBehaviour
{

    // The level number the portal will take you to
    [SerializeField]
    [Range(1,10)]
    int levelNumber;

    // The UI Pop Up that appears when the player enters a level portal
    public Menu.Page levelPortalPopUp;
    // UI Pop up elements
    private GameObject canEnterLevelUI;
    private GameObject cannotEnterLevelUI;

    void Start() {
        Transform popUpBackground = levelPortalPopUp.gameObject.transform.GetChild(0);
        foreach(Transform t in popUpBackground) {
            if (t.tag == "CanEnterLevelUI") {
                canEnterLevelUI = t.gameObject;
            } else if (t.tag == "CannotEnterLevelUI") {
                cannotEnterLevelUI = t.gameObject;
            }
        }

        if (canEnterLevelUI == null || cannotEnterLevelUI == null) {
            throw new System.Exception("Couldn't find all the UI Elements for the Level Portal Pop Up");
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            levelPortalPopUp.gameObject.SetActive(true);
            
            Transform popUpBackground = levelPortalPopUp.gameObject.transform.GetChild(0);
            
            // Set the variable in the level manager if the player is above a portal
            LevelManager.instance.SetPortalLevelNumber(levelNumber);
            
            if (LevelManager.instance.CanPlayerEnterLevel(levelNumber)) {
                canEnterLevelUI.SetActive(true);
                cannotEnterLevelUI.SetActive(false);
                canEnterLevelUI.GetComponent<TMP_Text>().text = "Enter Level " + levelNumber + "?";
            } else {
                canEnterLevelUI.SetActive(false);
                cannotEnterLevelUI.SetActive(true);
                cannotEnterLevelUI.GetComponent<TMP_Text>().text = "Can't Enter Level " + levelNumber;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            levelPortalPopUp.gameObject.SetActive(false);
        }
    }

    public int GetLevelNumber() {
        return levelNumber;
    }
}
