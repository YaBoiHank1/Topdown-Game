using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : MonoBehaviour
{
    public Sprite capSprite;
    public Sprite cappingSprite;
    private Sprite startSprite;
    public bool capped;
    public float capTime = 5f;
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        startSprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (capped)
        {
            GetComponent<SpriteRenderer>().sprite = capSprite;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !capped)
        {
            GetComponent<SpriteRenderer>().sprite = cappingSprite;
            timer += Time.deltaTime;
            if (timer >= capTime)
            {
                capped = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (timer <= capTime && !capped)
            {
                GetComponent<SpriteRenderer>().sprite = startSprite;
                timer = 0;
            }
        }
    }
}
