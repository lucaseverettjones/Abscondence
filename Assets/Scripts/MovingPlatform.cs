using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] points = new Transform[2]; // positions the platform moves between and stops at; 0 - starting point; add more in the Inspector if necessary
    [SerializeField] private float speed; // how fast the platform moves
    [SerializeField] private float pauseTime; // how long the platform stays still before moving to the next point

    private bool moving; // true if the platform is moving to the next point
    [SerializeField] private bool playerActivated; // if true, the platform does not start moving until the player stands on it. if false, the platform starts moving immediately.

    private int i = 0; // index of the platform's current location(if not moving) or destination (if moving)
    
    void Start()
    {
        if (points.Length > 0)
        {
            transform.position = points[0].position; // the platform starts at the first point
        }
        if (playerActivated) // if the platform is player activated, it stays still. if not, it starts moving to the next point right away.
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
    }

    void Update()
    {
        if (moving && points.Length > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime); // move towards the next point
            if (Vector3.Distance(transform.position, points[i].position) < 0.01f)
            {
                // once the platform is close enough to its destination, it stops moving for a few seconds, then starts moving to the next point
                transform.position = points[i].position;
                moving = false;
                Invoke("ToNextPoint", pauseTime);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) // when the player lands on the platform, parent the player to the platform so the platform carries the player along
        {
            other.transform.parent = this.transform;
            if (playerActivated) // if the platform is player activated, it starts moving when the player lands on it
            {
                moving = true;
                playerActivated = false; // this ensures that the platform will not move when the player lands on it while it is waiting at one of the points.
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) // when the player leaves the platform, unparent the player so the platform does not influence their movement
        {
            other.transform.parent = null;
        }
    }

    private void ToNextPoint() // the platform starts moving towards the next point in the sequence
    {
        i++;
        if (i >= points.Length) // if the platform is at the last point, it moves towards the first point
        {
            i = 0;
        }
        moving = true;
    }
}
