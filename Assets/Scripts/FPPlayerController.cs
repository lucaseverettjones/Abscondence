using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPPlayerController : MonoBehaviour
{
    Transform playerTransform;
    Rigidbody rb;

    float currentSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpHeight;

    float rotationX = 0f;
    float rotationY = 0f;

    [SerializeField] float sensitivity = 5f;

    bool onGround;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();




    }

    // Update is called once per frame
    void Update()
    {

        //mouse input
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);

        rotationY = Mathf.Clamp(rotationY, -90f, 90);


        //Player Movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MovePlayerForwards();
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MovePlayerBackwards();
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MovePlayerLeft();
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MovePlayerRight();
        }

        if (Input.GetKey(KeyCode.Space) && onGround == true)
        {
            PlayerJump();
            onGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }

        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    void PlayerJump()
    {
        rb.AddForce(Vector3.up * jumpHeight);
    }

    void MovePlayerForwards()
    {
        playerTransform.position += playerTransform.forward * maxSpeed * Time.deltaTime;
    }

    void MovePlayerBackwards()
    {
        playerTransform.position -= playerTransform.forward * maxSpeed * Time.deltaTime;
    }

    void MovePlayerRight()
    {
        playerTransform.position += playerTransform.right * maxSpeed * Time.deltaTime;
    }

    void MovePlayerLeft()
    {
        playerTransform.position -= playerTransform.right * maxSpeed * Time.deltaTime;
    }

}