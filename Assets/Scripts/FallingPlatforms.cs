using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    //public float fallDelay = 1000.0f;
    //private float fallDelayCount = 0.0f;
    private bool fall = false;
    private Animator anim;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
        {
            Destroy(gameObject);
        }

        /*
        if (fall)
        {
            fallDelayCount++;
        }
        
        if (fallDelay <= fallDelayCount)
        {
            Debug.Log("destroy platform");
            Destroy(gameObject);
        }*/
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            fall = true;
            anim.SetBool("fall", fall);
        }
    }
}
