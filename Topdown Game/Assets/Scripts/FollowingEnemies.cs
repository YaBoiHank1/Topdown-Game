using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemies : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject enemyProjectilePosition;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float range = 10f;
    [SerializeField] Transform target;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] public GameObject healthPrefab;
    [SerializeField] public GameObject ammoPrefab;

    private Rigidbody2D myRigidbody;
    BoxCollider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        enemyAnimator.SetBool("isShooting", false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, target.position) <= range)
        {
            MoveTowardsTarget();
            RotateTowardsTarget();
            countDownAndShoot();
            enemyAnimator.SetBool("isMoving", true);
        }

        if (Vector3.Distance(transform.position, target.position) > range)
        {
            enemyAnimator.SetBool("isMoving", false);
        }

        if (shotCounter <= .02f)
        {
            enemyAnimator.SetBool("isShooting", true);
            enemyAnimator.SetBool("isMoving", false);
        }
        else if (shotCounter > .02f)
        {
            enemyAnimator.SetBool("isShooting", false);
        }

    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void RotateTowardsTarget()
    {
        var offset = 270f;
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void countDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        AudioSource.PlayClipAtPoint(shootSFX, gameObject.transform.position);
        GameObject EnemyProjectile = Instantiate(projectile, enemyProjectilePosition.transform.position, Quaternion.identity) as GameObject;
        EnemyProjectile.GetComponent<Rigidbody2D>().velocity = gameObject.transform.up * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        processHit(damageDealer);
    }

    private void processHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
            int r = Random.Range(0, 10);
            if (r < 3)
            {
                GameObject AmmoPoweup = Instantiate(ammoPrefab, enemyProjectilePosition.transform.position, Quaternion.identity) as GameObject;

            }
            if (r > 6)
            {
                GameObject HealthPowerup = Instantiate(healthPrefab, enemyProjectilePosition.transform.position, Quaternion.identity) as GameObject;

            }
        }
    }


}
