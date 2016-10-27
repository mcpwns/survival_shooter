using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    /// <summary>
    /// like FixedUpdate, function is always called by Unity. 
    /// </summary>
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Fixed update is called every update for the physics in Unity. Player has a rigidbody which is under physics so use FixedUpdate to move him
    /// </summary>
    void FixedUpdate() 
    {
        /// <c>
        /// Axis is input. multiple Axis, horizontal, vertical, jump, fire1, fire2
        /// </c>
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    /// <summary>
    /// Move the character. Function is called in FixedUpdate
    /// </summary>
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        /// <movement>
        /// movement is set to normalized so diagonal movement isnt advantageous. speed is our speed not Unity speed. Time.deltatime is so that it is not moving 6units every update but
        /// one unit per update. basically slowing it down since every update is a 50th of a second. so 6*50th of a second. Every 50 50ths(25s?) of a second it will move 6 units. 
        /// </movement>
        movement = movement.normalized*speed*Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        ///<summary>
        ///Ray is a line from Camera(person playing) through screen to the point of mouse position. The Raycasthit is variable that detects if the mouse is hitting an object
        ///this if statement is used to check if mouse is hitting hte floor and returns true if it is and false if not
        /// </summary>
        if(Physics.Raycast (camRay,out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position; 
            playerToMouse.y = 0f; //so player doesn't start leaning back

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse); //makes the forward vector the playertoMouse. so player always moves according to where they're facing/looking
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f; //if either horizontal axis is pressed or vertical then its true.
        anim.SetBool("IsWalking", walking);
    }
}
