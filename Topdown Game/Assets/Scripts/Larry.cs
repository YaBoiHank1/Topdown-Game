using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larry : MonoBehaviour
{
    public int Ammo = 5;
    public float moveSpeed = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ammo")
        {
            Ammo += 5;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "SpeedBoost")
        {
            StartCoroutine(speedBoost());
            Destroy(collision.gameObject);
                
        }
    }

    IEnumerator speedBoost()
    {
        moveSpeed += 1;
        yield return new WaitForSeconds(5);
        moveSpeed -= 1;
    }
}
