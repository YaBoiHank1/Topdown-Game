using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : MonoBehaviour
{
    public Color capColor;
    public Color cappingColor;
    private Color startColor;
    public bool capped;
    public float capTime = 5f;
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        startColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (capped)
        {
            GetComponent<SpriteRenderer>().color = capColor;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !capped)
        {
            GetComponent<SpriteRenderer>().color = cappingColor;
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
                GetComponent<SpriteRenderer>().color = startColor;
                timer = 0;
            }
        }
    }
}
