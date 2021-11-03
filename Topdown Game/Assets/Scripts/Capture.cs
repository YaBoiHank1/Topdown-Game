using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : MonoBehaviour
{
    public Sprite capSprite;
    public Sprite cappingSprite;
    private Sprite startSprite;
    public bool capped;
    public bool spawnEnemies;
    public float capTime = 5f;
    public float timer = 0;
    GameSession gameSession;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float interval = 100;
    float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemies = true;
        startSprite = GetComponent<SpriteRenderer>().sprite;
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnEnemies == true && !capped)
        {
            counter += 1;
            if (counter >= interval)
            {
                 counter = 0;
                 Instantiate(enemyPrefab, transform.position, transform.rotation);
            }
        }
        
        if (capped)
        {
            GetComponent<SpriteRenderer>().sprite = capSprite;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !capped)
        {
            spawnEnemies = false;
            GetComponent<SpriteRenderer>().sprite = cappingSprite;
            timer += Time.deltaTime;
            if (timer >= capTime)
            {
                capped = true;
                gameSession.cappedPoints++;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spawnEnemies = true;
            if (timer <= capTime && !capped)
            {
                GetComponent<SpriteRenderer>().sprite = startSprite;
                timer = 0;
            }
        }
    }
}
