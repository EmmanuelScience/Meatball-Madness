using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFell : MonoBehaviour
{
    void OnTriggerEnter(Collider other) { 
            if (other.tag == "Player") {
                RestartScene();
            }
        }
    
    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


