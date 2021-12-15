using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageLives : MonoBehaviour
{
    // List of the heart objects representing lives
    // PUT THE LIVES FROM YOUR FIRST TO LAST LIFE
    public GameObject[] lives;

    // Reference to script that tracks player lives
    public char_obj char_Obj;

    // Max lives the player can have
    int maxLives;

    void Start()
    {
        // Decide what lives to show and which ones to hide
        for (int i = 0; i < lives.Length; i++) {
            if (i >= char_Obj.lives) {
                lives[i].SetActive(false);
            } else {
                lives[i].SetActive(true);
            }
        }

        maxLives = lives.Length;
    }

    // Add a life to the player
    public void AddLife() {
        if (char_Obj.lives < maxLives) {
            lives[char_Obj.lives].SetActive(true);
            char_Obj.lives++;
            Audio.AudioController.instance.PlayAudio(Audio.AudioType.HealthPickUp, false, 0.0f);
        } else {
            Debug.Log("Can't Add Anymore Lives");
        }
    }

    // Remove a life from the player
    public void RemoveLife() {
        if (char_Obj.lives > 0) {
            lives[char_Obj.lives - 1].SetActive(false);
            char_Obj.lives--;
        } else {
            Debug.Log("No More Lives to Remove");
        }
    }
}
