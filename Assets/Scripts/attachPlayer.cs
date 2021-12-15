using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachPlayer : MonoBehaviour
{
    public GameObject player;

    /*private void onTriggerEnter(Collider other){
        /*if(other.gameObject == player){
            
            player.transform.localPosition = transform.localPosition;
        }
        Debug.Log("the player " );
        
    }

    private void onTriggerExit(Collider other){
        if(other.gameObject == player){
            player.transform.parent = null;
        }
    }*/

    void OnTriggerEnter(Collider other) {
         if (other.transform.tag == "Player") {
             Debug.Log("working " + other.transform.tag);
             //other.transform.parent = transform;
            other.transform.SetParent(transform);
            /*if (other.gameObject.GetComponent<Rigidbody>() != null) {
         gameObject.AddComponent<FixedJoint> ();  
         gameObject.GetComponent<FixedJoint>().connectedBody = player.gameObject.GetComponent<Rigidbody>();;
             //other.rigidbody.useGravity = false;
            }*/
         }
     }
 
     private void OnTriggerExit(Collider other) {
         if (other.transform.tag == "Player") {
             //other.transform.parent = null;
             other.transform.SetParent(null);
             //other.rigidbody.useGravity = true;
         }
     }
    
}
