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
    float range = 10f;
    GameSession gameSession;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float minTime = 1000f;
    [SerializeField] float maxTime = 10000f;
    float Etimer;
    // Start is called before the first frame update
    void Start()
    {
        Etimer = Random.Range(minTime, maxTime);
        spawnEnemies = true;
        startSprite = GetComponent<SpriteRenderer>().sprite;
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnEnemies == true && !capped)
        {
            StartCoroutine(SpawnEnemies());
        }
        else if (!spawnEnemies )
        {
            StopCoroutine(SpawnEnemies());
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
                spawnEnemies = false;
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
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Etimer = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(Etimer);
            Vector3 randomPos = Random.insideUnitCircle * range;
            randomPos.z = 0f;
            Instantiate(enemyPrefab, transform.position + randomPos, transform.rotation);
        }
    }
}
