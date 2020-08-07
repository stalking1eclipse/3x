using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimb : MonoBehaviour
{

    Animator anim;
    bool canClimb = false;
    bool climb = false;
    Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveOnWall();
        anim.SetBool("CanClimb", canClimb);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            climb = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
          //  climb = false;
          //  canClimb = false;
        }
            
    }

    void moveOnWall()
    {
        if (climb && Input.GetKey(KeyCode.W))
        {
            canClimb = true;
            playerRb.useGravity = false;
        }

        else if (Input.GetKeyUp(KeyCode.W))
        {
            canClimb = false;
            playerRb.useGravity = true;
        }
    }

}
