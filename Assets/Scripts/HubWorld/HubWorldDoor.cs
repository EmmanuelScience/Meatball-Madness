using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The script that controls variables for HubWorldDoor
public class HubWorldDoor : MonoBehaviour
{
    public bool isOpen {get; private set;} = false;

    // Level Portal that is associated with door
    public LevelPortal levelPortal;

    public void OpenDoor() {
        if (!isOpen) {
            Vector3 rotateVector = new Vector3(0.0f,-90f,0.0f);
            this.transform.Rotate(rotateVector, Space.World);
            isOpen = true;
        }
    }   

    public void CloseDoor() {
        if (isOpen) {
            Vector3 rotateVector = new Vector3(0.0f,90f,0.0f);
            this.transform.Rotate(rotateVector, Space.World);
            isOpen = false;
        }
    }



}
