using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Open door");
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.Play("OpenDoor");
            }
        }
       
    }
}
