using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    [Header("Player Projectile")]
    [SerializeField] float projectileSpeed;
    [SerializeField] float reloadTime = 3f;
    [SerializeField] public int ammo = 1;
    [SerializeField] GameObject cannonballPrefab;
    [SerializeField] GameObject caseshotPrefab;
    [SerializeField] GameObject projectilePosition;
    [Header("Player Health")]
    [SerializeField] public int playerHealth = 10;
    [SerializeField] Text healthText;
    [Header("Other")]
    [SerializeField] AudioClip shootSFX;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas deathCanvas;
    [Header("Deez")]
    [SerializeField] int Nuts;
    
    bool escPressed;
    bool tabPressed;
    bool isAlive;
    EdgeCollider2D myCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        escPressed = false;
        tabPressed = false;
        pauseCanvas.enabled = false;
        deathCanvas.enabled = false;
        isAlive = true;
        myCollider = GetComponent<EdgeCollider2D>();
        healthText.text = "Health: " + playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        TakeDamage();
        Move();
        Pause();
        Die();
        Fire();
        AmmoSwitch();
        //LookAtMouse();


    }

   
    private IEnumerator reload()
    {
        yield return new WaitForSeconds(reloadTime);
        ammo = 1;
        Debug.Log(ammo);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && ammo > 0)
        {
            if (tabPressed == false)
            {
                GameObject cannonBall = Instantiate(cannonballPrefab, projectilePosition.transform.position, Quaternion.identity) as GameObject;
                cannonBall.GetComponent<Rigidbody2D>().velocity = gameObject.transform.up * projectileSpeed;
                AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position);
                ammo = 0;
                Debug.Log(ammo);
            }
            else if (tabPressed == true)
            {
                GameObject caseshot = Instantiate(caseshotPrefab, projectilePosition.transform.position, Quaternion.identity) as GameObject;
                caseshot.GetComponent<Rigidbody2D>().velocity = gameObject.transform.up * projectileSpeed;
                AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position);
                ammo = 0;
                Debug.Log(ammo);
            }
        }
        else if (ammo <= 0 && Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(reload());

        }
    }

    private void AmmoSwitch()
    {
        if (Input.GetKey(KeyCode.Tab) && tabPressed == false)
        {
            tabPressed = true;
            Debug.Log("tabPressed is true");
        }
        else if (Input.GetKey(KeyCode.Tab) && tabPressed == true)
        {
            tabPressed = false;
            Debug.Log("tabPressed is false");
        }
    }

    

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = transform.position.x + deltaX;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;
        transform.position = new Vector2(newXPos, newYPos);

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.back * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
    }

    //private void LookAtMouse()
    //{
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        //transform.up = direction;
    //}

    private void TakeDamage()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Hazards", "Enemies", "Enemy Projectiles")))
        {
            playerHealth--;
            healthText.text = "Health: " + playerHealth;
            Debug.Log(playerHealth);
        }
    }

    private void Die()
    {
        if (playerHealth <= 0)
        {
            deathCanvas.enabled = true;
            isAlive = false;
            
        }
    }

    private void Pause()
    {
        if (Input.GetButtonDown("Cancel") && escPressed == false)
        {
            Time.timeScale = 0;
            pauseCanvas.enabled = true;
            escPressed = true;
        }
        else if (Input.GetButtonDown("Cancel") && escPressed == true)
        {
            Time.timeScale = 1;
            pauseCanvas.enabled = false;
            escPressed = false;
        }
    }




}
