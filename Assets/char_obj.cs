using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class char_obj : MonoBehaviour
{
    public int lives;
    private float hit_timestamp;
    private float hit_cooldown = .5f;
    public frog_state_machine frog;

    public ManageLives manageLives;
    public GameObject hit_screen;
    
    // Start is called before the first frame update
    void Start()
    {
        hit_timestamp = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0) {
            die();
        }
        var color = hit_screen.GetComponent<Image>().color;
        color.a /= 1.002F;
        hit_screen.GetComponent<Image>().color = color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tongue" && Time.time > hit_timestamp + hit_cooldown) {
            hit_timestamp = Time.time;
            hit();
        }
    }
    private void hit() {
        manageLives.RemoveLife();
        print("Player: hit");
        frog.hitPlayer();
        var color = hit_screen.GetComponent<Image>().color;
        color.a = 0.8F;
        hit_screen.GetComponent<Image>().color = color;
        Audio.AudioController.instance.PlayAudio(Audio.AudioType.Player_Hit, false, 0.0f);
    }
    private void die() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        print("Player: died");
    }
}
