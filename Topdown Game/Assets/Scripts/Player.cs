using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float playerHealth = 10f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject projectilePosition;
    [SerializeField] float projectileSpeed;
    [SerializeField] public int ammo = 1;
    [SerializeField] float reloadTime = 3f;

    EdgeCollider2D myCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage();
        Move();
        LookAtMouse();
        
        if (Input.GetButtonDown("Fire1") && ammo > 0)
        {
            Fire();
            ammo = 0;
            Debug.Log(ammo);
        }
        else if (ammo <= 0 && Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(reload());
            
        }
    }

    private IEnumerator reload()
    {
        yield return new WaitForSeconds(reloadTime);
        ammo = 1;
        Debug.Log(ammo);
    }

    private void Fire()
    {
        GameObject cannonBall = Instantiate(projectilePrefab, projectilePosition.transform.position, Quaternion.identity) as GameObject;
        cannonBall.GetComponent<Rigidbody2D>().velocity = gameObject.transform.up * projectileSpeed;
    }
    
    
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = transform.position.x + deltaX;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        transform.up = direction;
    }

    private void TakeDamage()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Hazards", "Enemies", "Enemy Projectiles")))
        {
            playerHealth--;
            Debug.Log(playerHealth);
        }
    }

    
}
