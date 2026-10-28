using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public int keys = 0; // how many keys the player is holding

    [SerializeField] Animator anim;
    [SerializeField] AudioClip deathSound;
    TPPlayerController player;
    AudioSource playerSource;

    

    void Start()
    {
        player = gameObject.GetComponent<TPPlayerController>();
        playerSource = gameObject.GetComponent<AudioSource>();
        
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        else if(Input.GetKey(KeyCode.R))
        {
            ResetScene();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key")) // if the player touches a key, the key disappears from the scene and is added to their number of keys.
        {
            keys++;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Credits");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard") && !player.godMode) // if the player touches a hazard, they die (incomplete)
        {
            anim.SetBool("Dead", true); // start death animation
            player.canMove = false; // stop the player from moving
            playerSource.PlayOneShot(deathSound);
            Invoke("ResetScene", 2f); // reset the scene after 2 seconds
        }
    }

    void ResetScene() // reset the current scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
