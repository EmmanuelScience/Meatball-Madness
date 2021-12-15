using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public ManageLives manageLives;

    void OnTriggerEnter(Collider other) { 
        if (other.tag == "Player") {
            manageLives.AddLife();
            Destroy(gameObject);
        }
    }
}

