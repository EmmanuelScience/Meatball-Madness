using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuButton : MonoBehaviour
{

    public TMP_Text text;

    void Start()
    {
        text.color = Color.red;
    }

    public void OnMouseEnter()
    {
        text.color = Color.green;
    }

    public void OnMouseExit()
    {
        text.color = Color.red;
    }


    public void PressStart() {
        SceneManager.LoadScene("Tutorial");
    }
    
}
