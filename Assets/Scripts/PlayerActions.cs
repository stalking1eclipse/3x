using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject shield;
    Animator anim;

    int weaponTracker=-1;
    bool blockAvailable = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switchWeapons();

        abilities();
    }


    void switchWeapons()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
                weaponTracker++;

            if (weaponTracker > weapons.Length-1)
            {
                weaponTracker = 0;
            }
                 getWeapon(weaponTracker);
        }
    }

    void abilities()
    {
        if (blockAvailable)
        {
            if (Input.GetMouseButton(1))
            {
                anim.SetBool("block", true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                anim.SetBool("block", false);
            }

        }
    }

    void getWeapon(int weaponNum)
    {
     
        Debug.Log(weaponNum);
        for (int r=0; r<weapons.Length; r++)
        {
            weapons[r].SetActive(false);
        }

        weapons[weaponNum].SetActive(true);

        if (weapons[weaponNum].name == "blade")
        {
            anim.SetBool("Blade", true);
            shield.SetActive(true);
            blockAvailable = true;
           
        }
        else if (weapons[weaponNum].name != "blade")
        {
            anim.SetBool("Blade", false);
            shield.SetActive(false);
            blockAvailable = false;
        }

    }
}
