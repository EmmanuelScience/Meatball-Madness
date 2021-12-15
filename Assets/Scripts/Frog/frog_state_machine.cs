using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog_state_machine : MonoBehaviour
{
    public vertex start;
    public GameObject player;

    private enum state {attack, search, wait};
    private state curr_state;
    private float jump_timestamp;
    private float shot_timestamp;
    private float wait_timestamp;
    public tongue self_tongue;
    private navigation_component self_nav;

    public float shot_cooldown;
    public float jump_cooldown;
    public float wait_cooldown;
    public float tongue_len;
    public float tongue_speed;

    // Start is called before the first frame update
    void Start()
    {
        curr_state = state.search;
        jump_timestamp = Time.time;
        shot_timestamp = Time.time;
        self_nav = GetComponent<navigation_component>();
        // self_tongue = GetComponent<tongue>();
        this.transform.position = start.transform.position;
        self_nav.setCurrNode(start);
        self_tongue.setUp(tongue_len, tongue_speed);
    }

    // Update is called once per frame
    void Update()
    {
        print( can_reach());
        transform.LookAt(player.transform);
        if (curr_state == state.attack) {
            if (!can_reach() && self_tongue.GetState() == tongue.state.idle) {
                print("atk->srch");
                curr_state = state.search;
            } else if (can_reach() && self_tongue.GetState() == tongue.state.idle && Time.time > shot_timestamp + shot_cooldown) {
                shot_timestamp = Time.time;
                self_tongue.shoot(player);
                Audio.AudioController.instance.PlayAudio(Audio.AudioType.Frog_Attack, false, 0.0f); // Play frog attack sound
            }
        } else if (curr_state == state.search) {
            if (can_reach()) {
                print("srch->atk");
                curr_state = state.attack;
            } else if (Time.time > jump_timestamp + jump_cooldown) {
                jump_timestamp = Time.time;
                this.transform.position = self_nav.jumpNext(player);
                Audio.AudioController.instance.PlayAudio(Audio.AudioType.Frog_Move, false, 0.0f); // Play frog move sound
            }

        } else if (curr_state == state.wait) {
            if (Time.time > wait_timestamp + wait_cooldown) {
                curr_state = state.attack;
                print("wait->atk");
            }

        }
    }
    bool can_reach() {
        bool see = !Physics.Linecast(this.transform.position+2*Vector3.up, player.transform.position+2*Vector3.up);
        bool reach = Vector3.Distance(this.transform.position, player.transform.position) + 1f < tongue_len;
        return see && reach;
    }
    public void hitPlayer() {
        print("Frog: hit");
        curr_state = state.wait;
        wait_timestamp = Time.time;
    }
}
