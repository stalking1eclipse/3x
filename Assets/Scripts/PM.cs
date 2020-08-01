using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM : MonoBehaviour
{

    float vertical;
    float horizontal;
    CapsuleCollider capsuleCollider;
    BoxCollider boxCollider;
    Rigidbody rb;

    bool onGround = true;
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
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        movePlayer();

        if (vertical != 0)
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
        if (onGround == false)
        {
            transform.Translate(Vector3.forward * vertical * speed * Time.deltaTime);
            transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
        }
        
        anim.SetInteger("SpeedUp", SpeedUp);
        anim.SetFloat("Speed", vertical);
        anim.SetBool("OnGround", onGround);
        Debug.Log(onGround);
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

        if (Input.GetKeyDown(KeyCode.Space)&&onGround)
        {
            onGround = false;
            rb.AddForce(Vector3.up*7,ForceMode.Impulse);
        }
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

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

    

    IEnumerator getUp()
    {
        yield return new WaitForSeconds(.8f);

        capsuleCollider.enabled = true;
        boxCollider.enabled = false;
    }
}
