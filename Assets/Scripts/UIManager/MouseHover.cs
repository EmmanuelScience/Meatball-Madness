using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MouseHover : MonoBehaviour
{
    public bool start;
    public bool win;
    //public TextMeshPro text;
    public TMP_Text text;   

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        if (start)
        {
            text.color = Color.red;
        } 
        else if (win)
        {
            text.color = new Color(211f/255f, 199f/255f, 41f/255f);
        }
    }

    public void OnMouseEnter()
    {
        text.color = Color.green;
    }

    public void OnMouseExit()
    {
        if (start)
        {
            text.color = Color.red;
        }
        else if (win)
        {
            text.color = new Color(211f / 255f, 199f / 255f, 41f / 255f);
        }
    }

    public void PressStart() {
        SceneManager.LoadScene("Tutorial");
    }
}
