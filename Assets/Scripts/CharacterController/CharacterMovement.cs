using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    // States for the Character Controller
    public enum CharacterControllerStates {
        GROUNDED,
        SINGLE_JUMP,
        DOUBLE_JUMP,
        NONE
    };

    // Current character controller state
    public CharacterControllerStates currState;

    public CharacterController characterController;
    public Transform cam;
    public float speed;
    public float turnSmoothTime = 0.1f;
    public float singleJumpHeight;
    public float doubleJumpHeight;
    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }

    private bool onIce = false;
    private Vector3 oldDirection = new Vector3(0, 0, 0);
    private Vector3 iceMomentum = new Vector3(0, 0, 0);
    private bool newIceContact = false;

    private float turnSmoothVelocity;
    private Vector3 playerVelocity;
    private int groundContactCount = 0;

    void Start() {
        // The player is at the NONE state until touching the ground when first spawning
        currState = CharacterControllerStates.NONE;
    }

    
    // Update is called once per frame
    void Update()
    {   
        // Change state to GROUNDED if player hits the ground
        if (characterController.isGrounded) {
            currState = CharacterControllerStates.GROUNDED;
        }

        // Logic that runs when player is grounded
        if (currState == CharacterControllerStates.GROUNDED) {

            if (characterController.isGrounded && playerVelocity.y < 0) {
                playerVelocity.y = 0f;
            }
            
            if  (Input.GetButtonDown("Jump")) {
                playerVelocity.y = 0f;
                playerVelocity.y += Mathf.Sqrt(singleJumpHeight * -2 * Physics.gravity.y);
                Audio.AudioController.instance.PlayAudio(Audio.AudioType.Player_Jump, false, 0.0f); // Play jump sound
                currState = CharacterControllerStates.SINGLE_JUMP;
            }


        // Logic that runs when player has performed a single jump
        } else if (currState == CharacterControllerStates.SINGLE_JUMP) {
            if  (Input.GetButtonDown("Jump")) {
                playerVelocity.y = 0f;
                playerVelocity.y += Mathf.Sqrt(doubleJumpHeight * -2 * Physics.gravity.y);
                Audio.AudioController.instance.PlayAudio(Audio.AudioType.Player_DoubleJump, false, 0.0f); // Play jump sound
                currState = CharacterControllerStates.DOUBLE_JUMP;
            }
        
        // Logic that occurs when player has performed a double jump
        } else if (currState == CharacterControllerStates.DOUBLE_JUMP) {

        }

        Move();
    }

    // Runs every frame and controls player movement
    private void Move() {
        float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
        float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis
        Vector3 direction = new Vector3(h, 0, v);

        if (onIce)
        {
            if (direction.magnitude <= characterController.minMoveDistance)
            {
                characterController.Move(oldDirection.normalized * speed * Time.deltaTime);
            } 
        }

        if (direction.magnitude >= characterController.minMoveDistance) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(direction.normalized * speed * Time.deltaTime);
        }

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
        
        if (direction.magnitude != 0)
        {
            oldDirection = direction;
        }
    }

    //This is a physics callback
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Ground")
        {
            ++groundContactCount;
        } else if (other.transform.gameObject.tag == "Bounce")
        {
            playerVelocity.y = 0f;
            playerVelocity.y += Mathf.Sqrt(singleJumpHeight * -3 * Physics.gravity.y);
            Audio.AudioController.instance.PlayAudio(Audio.AudioType.Player_Jump, false, 0.0f); // Play jump sound
            currState = CharacterControllerStates.SINGLE_JUMP;
        } else if (other.transform.gameObject.tag == "Ice")
        {
            ++groundContactCount;
            onIce = true;
            newIceContact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Ground")
        {
            --groundContactCount;
        } else if (other.transform.gameObject.tag == "Ice")
        {
            --groundContactCount;
            onIce = false;
        }
    }
}
