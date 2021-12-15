using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{

    public TMP_Text tutorialPopUpText;
    public string instructions;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            Menu.PageController.instance.TurnPageOn(Menu.PageType.TutorialPopUp);
            tutorialPopUpText.text = instructions;
            Time.timeScale = 0;
            Destroy(gameObject);
        }
						
    }
}
