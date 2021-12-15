using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private enum State
    {
        MOVE_FORWARD,
        MOVE_BACK,
        PAUSE
    };
    private State state = State.PAUSE;
    
    public float pauseTimer = 20.0f;
    private float pauseCount;

    private Vector3 startPos;
    public Vector3 endPos;

    public float speed = 0.1f;

    private Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        pauseCount = 0.0f;
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(0, 0, 0);
        switch(state)
        {
            case State.MOVE_FORWARD:
                move = Vector3.Normalize(endPos - startPos) * speed;
                this.transform.position = this.transform.position + move;
                if (Vector3.Distance(startPos, this.transform.position) >= Vector3.Distance(startPos, endPos))
                {
                    state = State.PAUSE;
                    this.transform.position = endPos;
                }
                break;
            case State.MOVE_BACK:
                move = Vector3.Normalize(startPos - endPos) * speed;
                this.transform.position = this.transform.position + move;
                if (Vector3.Distance(endPos, this.transform.position) >= Vector3.Distance(endPos, startPos))
                {
                    state = State.PAUSE;
                    this.transform.position = startPos;
                }
                break;
            case State.PAUSE:
                pauseCount++;
                if (pauseCount >= pauseTimer)
                {
                    if (this.transform.position == startPos)
                    {
                        state = State.MOVE_FORWARD;
                        pauseCount = 0.0f;
                    }
                    else if (this.transform.position == endPos)
                    {
                        state = State.MOVE_BACK;
                        pauseCount = 0.0f;
                    }
                }
                break;
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
