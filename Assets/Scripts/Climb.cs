using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{

    public bool canClimb = false;
    float vertical;
    public Transform player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        climb();
    }


    void climb()
    {
        //if (canClimb)
            anim.SetBool("CanClimb", canClimb);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && vertical > 0)
            canClimb = true;
        else if (vertical <= 0)
            canClimb = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            canClimb = false;
    }
}
