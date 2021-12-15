using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tongue : MonoBehaviour
{
    public GameObject frog;
    public GameObject projectile;
    public float tpf;
    public float tpu;
    private float timestamp;
    private float speed;
    private float length;
    public enum state {idle, shoot, recall};
    private state curr_state;
    public void setUp(float l, float s) {
        length = l;
        speed = s;
    }

    // Use this for initialization
    void Start () {
        reset();
        timestamp = Time.time;
    }

    // Update is called once per frame
    void Update () {
        // print(curr_state);
        if (curr_state == state.idle){
            // if(Time.time > timestamp + shot_cooldown){
            //     if (canSeeTarget()) {
            //         shoot();
            //     }
            // }
        } else if (curr_state == state.shoot) {
            if(Vector3.Distance(frog.transform.position, projectile.transform.position) > length) {
                recall();
            }
        } else if (curr_state == state.recall) {
            if (Vector3.Distance(frog.transform.position, projectile.transform.position) < 1) {
                reset();
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            // print("hit");
        }
        recall();
    }
    void reset(){
        // print("reset");
        curr_state = state.idle;
        print(curr_state);
        projectile.transform.position = frog.transform.position + tpf * frog.transform.forward + tpu * frog.transform.up;
        projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    void recall() {
        // print("recall");
        curr_state = state.recall;
        print(curr_state);
        float launchVelocity = speed;
        projectile.GetComponent<Rigidbody>().velocity = (launchVelocity * (frog.transform.position - projectile.transform.position) / Vector3.Magnitude((frog.transform.position - projectile.transform.position)));

    }
    public void shoot(GameObject target){
        // print("shoot");
        timestamp = Time.time;
        curr_state = state.shoot;
        print(curr_state);
        float launchVelocity = speed;
        projectile.GetComponent<Rigidbody>().velocity = (launchVelocity * (target.transform.position - projectile.transform.position) / Vector3.Magnitude((target.transform.position - projectile.transform.position)));
    }
    bool canSeeTarget(GameObject target) {
        bool ans = !Physics.Linecast(frog.transform.position+2*Vector3.up, target.transform.position+2*Vector3.up);
        return ans;
    }
    public state GetState() {
        return curr_state;
    }
}
