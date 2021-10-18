using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    [SerializeField] GameObject player;
    public float captureTime = 5f;
    public bool captured;
    public float CapRadius = 1f;
    public float playerCap;
    
    CircleCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        
        myCollider = GetComponent<CircleCollider2D>();
        captured = false;
        playerCap = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (captured == true)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (myCollider.bounds.Contains(player.transform.position))
        {
            playerCap += Time.deltaTime;
            
            if (playerCap == captureTime)
            {
                
                captured = true;
                playerCap = 0;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.tag == "player" && captured == false)
        {
            playerCap -= Time.deltaTime;
        }
    }
}
