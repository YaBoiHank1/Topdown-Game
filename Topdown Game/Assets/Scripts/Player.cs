using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject projectilePosition;
    [SerializeField] float projectileSpeed;
    [SerializeField] float fireRate = 0.5F;
    [SerializeField] float nextFire = 1.5F;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAtMouse();
        
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
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

    
}
