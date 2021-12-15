using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages all of the doors in the hubworld
public class HubWorldDoorManager : MonoBehaviour
{
    // List of all the doors in the scene, put all the doors in child objects of this script
    List<HubWorldDoor> doors = new List<HubWorldDoor>();

    void Start() {
        GetDoors();
    }

    // Get references to all the doors
    void GetDoors() {
        foreach (Transform child in transform) {
            HubWorldDoor door = child.gameObject.GetComponent<HubWorldDoor>();
            if (door == null) {
                throw new System.Exception("Couldn't find door. Make sure all child of objects of this gameobject are doors with the HubWorldDoor Script");
            } else {
                if (LevelManager.instance.CanPlayerEnterLevel(door.levelPortal.GetLevelNumber())) {
                    door.OpenDoor();
                } else {
                    door.CloseDoor();
                }
                doors.Add(door);
            }
        }
    }


}
