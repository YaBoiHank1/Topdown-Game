using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitch : MonoBehaviour
{
    public Sprite spriteA;
    public Sprite spriteB;
    bool toggle;
    private Image imageComponent;
    // Start is called before the first frame update
    public void Start()
    {
        imageComponent = GetComponent<Image>();
        toggle = false;
        if (imageComponent.sprite == null)
        {
            imageComponent.sprite = spriteB;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            toggle = !toggle;
        }
        Swap();
        
    }

    private void Swap()
    {
        if (toggle == false)
        {
            imageComponent.sprite = spriteB;
        }
        if (toggle == true)
        {
            imageComponent.sprite = spriteA;
        }
    }
}
