using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingPlatform : MonoBehaviour
{

    private enum anim_state
    {
        INIT_IDLE,
        INIT_TO_ALT,
        ALT_IDLE,
        ALT_TO_INIT
    }

    private anim_state state;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null) { Debug.Log("No Animator Found"); }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = anim_state.INIT_IDLE;
        animator.SetInteger("state", 0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("IdleInitial"))
        {
            state = anim_state.INIT_IDLE;
        } else if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("IdleAlternate"))
        {
            state = anim_state.ALT_IDLE;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            switch(state)
            {
                case anim_state.INIT_IDLE:
                    Debug.Log("FlipTriggered");
                    state = anim_state.INIT_TO_ALT;
                    animator.SetInteger("state", 1);
                    break;
                case anim_state.ALT_IDLE:
                    Debug.Log("FlipTriggered");
                    state = anim_state.ALT_TO_INIT;
                    animator.SetInteger("state", 3);
                    break;
            }
        }
        
    }
}
