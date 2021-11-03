using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffects : MonoBehaviour
{
    Animator myAnimator;
    public int ammo;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        ammo = FindObjectOfType<Player>().ammo;
        myAnimator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        ammo = FindObjectOfType<Player>().ammo;
        if (Input.GetButtonDown("Jump") && ammo >=1)
        {
            myAnimator.SetTrigger("Fire");
        }
    }
}
