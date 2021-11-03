using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float interval = 100;
    float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += 1;
        if(counter >= interval)
        {
            counter = 0;
            Instantiate(enemyPrefab, transform.position, transform.rotation);
        }
    }
}
