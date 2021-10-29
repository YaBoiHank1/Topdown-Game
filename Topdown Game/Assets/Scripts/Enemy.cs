using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject enemyProjectilePosition;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        enemyAnimator.SetBool("isShooting", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shotCounter <= .02f)
        {
            enemyAnimator.SetBool("isShooting", true);
        }

        else if (shotCounter > .02f)
        {
            enemyAnimator.SetBool("isShooting", false);
        }

        countDownAndShoot();
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
        }
    }
}
