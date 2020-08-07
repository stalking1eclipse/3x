using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM : MonoBehaviour
{

    float vertical;
    float horizontal;
    CapsuleCollider capsuleCollider;
    BoxCollider boxCollider;


    int SpeedUp = 0;
    int speed = 2;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        capsuleCollider = GetComponent<CapsuleCollider>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        movePlayer();

        if (vertical != 0 )
        {
            lookAtCam();
        }

        if (SpeedUp == 2)
        {
            speed = 3;
        }else
        {
            speed = 2;
        }
      
        
        anim.SetInteger("SpeedUp", SpeedUp);
        anim.SetFloat("Speed", vertical);
    }

    void movePlayer()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            if (SpeedUp == 2)
                SpeedUp = 0;

            else if (SpeedUp == 1)
                SpeedUp = 2;

            else if (SpeedUp == 0)
                SpeedUp = 1;
        }
        else if (Input.GetKeyUp(KeyCode.W))
            SpeedUp = 0;

        if (Input.GetKeyDown(KeyCode.Q)&& SpeedUp == 2) 
        {
            anim.SetTrigger("Slide");
            capsuleCollider.enabled = false;
            boxCollider.enabled = true;

            StartCoroutine("getUp");
        }
        
        
    }

    //camera look at player direction
    void lookAtCam()
    {
        Camera cam = Camera.main;
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;


        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection;

        moveDirection = forward * vertical + right * horizontal;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.5f);
    }

    
    //reactivates default collider after a slide
    IEnumerator getUp()
    {
        yield return new WaitForSeconds(.8f);

        capsuleCollider.enabled = true;
        boxCollider.enabled = false;
    }

}
