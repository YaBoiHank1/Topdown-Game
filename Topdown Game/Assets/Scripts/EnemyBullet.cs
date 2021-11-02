using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    CircleCollider2D bulletCollider;
    public float lifetime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        bulletCollider = GetComponent<CircleCollider2D>();
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Destroy(gameObject);
        }
    }
}
