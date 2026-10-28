using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TPPlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpHeight;

    private Rigidbody rb;

    [SerializeField] GameObject camera;
    [SerializeField] float cameraSpeed;
    [SerializeField] float cameraRotateSpeed;
    private Transform newCameraPosition;

    private Transform playerTransform;
    private bool canJump;
    public bool canMove;
    private bool facingRight;

    [SerializeField] private Animator anim;

    public bool godMode;
    [SerializeField]private bool noGravity;


    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (playerTransform == null)
        {
            playerTransform = GetComponent<Transform>();
        }

        camera = GameObject.FindGameObjectWithTag("Camera");
        canMove = true;
        facingRight = true;
        anim.SetBool("FacingRight", true);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && canMove)
        {
            Jump();
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && canMove)
        {
            MovePlayerRight();
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && canMove)
        {
            MovePlayerLeft();
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            anim.SetFloat("Speed", 0);
        }

        if (newCameraPosition != null)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, newCameraPosition.transform.position, cameraSpeed * Time.deltaTime);
            camera.GetComponent<Transform>().rotation = Quaternion.Slerp(camera.transform.rotation, newCameraPosition.rotation, cameraRotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.I))
        {
            godMode = true;
        }
       

        if (Input.GetKey(KeyCode.O))
        {
            noGravity = true;
            rb.useGravity = false;
        }
        


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room"))
        {
            newCameraPosition = other.transform;
            //camera.transform.position = other.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") && !canJump)
        {
            canJump= true;
            anim.SetBool("Grounded", true);
        }
        else if (canJump)
        {
            anim.SetBool("Collide", true);
        }
        // only plays if the player is on the ground, moving left/right, and collides with a collider
        // shifts from running animation to bump animation, then idle animation until the collision ends
    }

    private void OnCollisionExit(Collision collision)
    {
        anim.SetBool("Collide", false);
    }

    void Jump()
    {
        if(canJump)
        {
            canJump = false;
            anim.SetBool("Grounded", false);
            rb.AddForce(new Vector3(0,jumpHeight,0));
        }
    }

    void MovePlayerRight()
    {
        if (!facingRight)
        {
            facingRight = true;
            anim.SetBool("FacingRight", true);
        }

        rb.velocity = new Vector3(maxSpeed, rb.velocity.y, 0);
        anim.SetFloat("Speed", rb.velocity.x);
    }
    
    void MovePlayerLeft()
    {
        if (facingRight)
        {
            facingRight = false;
            anim.SetBool("FacingRight", false);
        }

        rb.velocity = new Vector3(-1 * maxSpeed, rb.velocity.y, 0);
        anim.SetFloat("Speed", rb.velocity.x * -1);
    }
}
