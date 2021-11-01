
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float rotationSpeed = 10f;

    [Header("Player Sprites")]
    [SerializeField] Sprite startSprite;
    [SerializeField] Sprite deathSprite;

    [Header("Player Projectile")]
    [SerializeField] float reloadTime = 3f;
    [SerializeField] public int ammo = 1;
    [SerializeField] GameObject projectilePosition;
    [Header("Cannonball")]
    [SerializeField] float cannonballSpeed;
    [SerializeField] GameObject cannonballPrefab;

    [Header("Caseshot")]
    [SerializeField] GameObject caseshotPrefab;
    [SerializeField] float caseshotSpeed;
    [SerializeField] float spread = 60f;
    [SerializeField] int bulletCount = 6;
    [SerializeField] float range = 10f;

    [Header("Player Health")]
    [SerializeField] public int playerHealth = 10;
    [SerializeField] Text healthText;
    [SerializeField] Image healthBar;

    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera mapCamera;

    [Header("Other")]
    [SerializeField] AudioClip shootSFX;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas deathCanvas;

    [Header("Deez")]
    [SerializeField] int Nuts;
    
    bool escPressed;
    bool fPressed;
    bool isAlive;
    
    BoxCollider2D myCollider;
    SpriteRenderer mySprite;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        mapCamera.enabled = false;
        escPressed = false;
        fPressed = false;
        pauseCanvas.enabled = false;
        deathCanvas.enabled = false;
        isAlive = true;
        mySprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
        healthText.text = "HP: " + playerHealth;
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
        healthBar.fillAmount = playerHealth;
        if (Input.GetKeyDown(KeyCode.F))
        {
            fPressed = !fPressed;
            Debug.Log("f " + fPressed);
        }
        if (isAlive)
        {
            mySprite.sprite = startSprite;
        }
        if (Input.GetButtonDown("Jump"))
        {
            mainCamera.enabled = !mainCamera.enabled;
            mapCamera.enabled = !mapCamera.enabled;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            mainCamera.enabled = !mainCamera.enabled;
            mapCamera.enabled = !mapCamera.enabled;
        }
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
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position);
            if (fPressed == false)
            {
                GameObject cannonBall = Instantiate(cannonballPrefab, projectilePosition.transform.position, Quaternion.identity) as GameObject;
                cannonBall.GetComponent<Rigidbody2D>().velocity = gameObject.transform.up * cannonballSpeed;
                ammo = 0;
                Debug.Log(ammo);
            }
            else if (fPressed == true)
            {
                float spreadRange = spread;
                for( var i = 0; i < bulletCount; i++)
                {
                    float variance = Random.Range(-spreadRange, spreadRange);
                    Vector3 randomPos = Random.insideUnitCircle * range;
                    randomPos.z = 0;
                    GameObject caseshot = Instantiate(caseshotPrefab, projectilePosition.transform.position + randomPos, Quaternion.identity) as GameObject;
                    caseshot.GetComponent<Rigidbody2D>().velocity = gameObject.transform.up * caseshotSpeed;
                    ammo = 0;
                    Debug.Log(ammo);
                }
                
            }
        }
        else if (ammo <= 0 && Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(reload());

        }
    }
    
    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * Time.deltaTime * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.up * Time.deltaTime * moveSpeed;
        }
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
            healthText.text = "HP: " + playerHealth;
            Debug.Log(playerHealth);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slow Hazard")
        {
            moveSpeed = 1.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slow Hazard")
        {
            moveSpeed = 3;
        }
    }

    private void Die()
    {
        if (playerHealth <= 0)
        {
            deathCanvas.enabled = true;
            isAlive = false;
            mySprite.sprite = deathSprite;
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
