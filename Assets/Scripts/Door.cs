using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool closed = true; // if the door has not been opened
    [SerializeField] private bool locked; // if the door needs a key to open
    private Animator anim;
    [SerializeField] GameObject minimapDoorObj;
    [SerializeField] Material unlockedDoorMat;

    void Start()
    {
        anim = transform.parent.gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerCollision>() != null && closed) // if the door is closed and the player touches it
        {
            if (locked && collision.gameObject.GetComponent<PlayerCollision>().keys > 0)
            {
                Open();
                collision.gameObject.GetComponent<PlayerCollision>().keys--;
                minimapDoorObj.GetComponent<Renderer>().material = unlockedDoorMat;
            }
            else if (!locked)
            {
                Open();
            }
        }
    }

    private void Open()
    {
        anim.SetTrigger("Open");
        closed = false;
    }
    private void Unlock()
    {
        anim.SetTrigger("Unlock");
        locked = false;
    }
}
/*
locked, keys

00 opens
01 opens
10 nothing
11 opens, lose a key
*/