using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloader : MonoBehaviour
{
    

    Animator myAnimator;
    public int ammo;

    // Start is called before the first frame update
    void Awake()
    {
        int ammo = FindObjectOfType<Player>().ammo;
    }

    public void Start()
    {
        
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        int ammo = FindObjectOfType<Player>().ammo;
        if (Input.GetButtonDown("Fire2") && ammo <= 0)
        {
            
            myAnimator.SetBool("reloading", true);
        }
        else if (ammo >= 1)
        {
            myAnimator.SetBool("reloading", false);
        }
    }
}
