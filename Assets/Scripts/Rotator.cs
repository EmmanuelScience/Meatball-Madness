using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public bool ccw;
    public float rotateSpeed = 0.5f;

    //public GameObject rotatingPlatform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ccw == true)
        {
            gameObject.transform.Rotate(0.0f, 1.0f * rotateSpeed, 0.0f, Space.Self);
        } else
        {
            gameObject.transform.Rotate(0.0f, -1.0f * rotateSpeed, 0.0f, Space.Self);
        }
    }

    void OnTriggerEnter(Collider other) {
         if (other.transform.tag == "Player") {
            other.transform.parent = this.transform;
         }
     }
 
     private void OnTriggerExit(Collider other) {
         if (other.transform.tag == "Player") {
            other.transform.parent = null;
         }
     }
}
