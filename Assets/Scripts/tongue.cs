using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    public GameObject eye;
    public GameObject targetHead;
    public GameObject target;
    public Rigidbody projectile;
    public float sleep_dur;
    private float timestamp;

    // Use this for initialization
    void Start () {
        timestamp = Time.time;

    }

    // Update is called once per frame
    void Update () {
        print(canSeeTarget());
        if(Time.time > timestamp + sleep_dur){
            timestamp = Time.time; 
            if (canSeeTarget()) {
                Rigidbody clone;
                clone = (Rigidbody)Instantiate(projectile, projectile.transform.position, projectile.rotation);

                clone.velocity = projectile.transform.TransformDirection (target.transform.position - projectile.transform.position);
            }
        }
    }

    bool canSeeTarget() {
        bool ans = !Physics.Linecast(eye.transform.position, targetHead.transform.position);
        return ans;
    }
}
