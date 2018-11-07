using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health = 50f;
    public Bullet bullet;
    public GameObject HitEffect;
    public GameObject DestroyEffect;
    public GameObject GameOverUI;
    public GameObject virus;
    public GameObject CardUI;
    public GameObject BossHandler;
    private float spawnRadius = 2;
    private Vector3 spawnPos;
    public float spawnRate;
    public float timeSpawn;



    private void Start()
    {
        InvokeRepeating("Spawn", timeSpawn, spawnRate);
    }

 

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        BossHandler.SetActive(false);
        GameObject destroyEffect;
        destroyEffect = Instantiate(DestroyEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        Destroy(destroyEffect, 2f);
        CardUI.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            GameObject hitEffect;
            hitEffect = Instantiate(HitEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            TakeDamage(bullet.damage);
            Destroy(hitEffect, 2f);
        }
    }

    void Spawn()
    {
       spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
       Instantiate(virus, spawnPos, Quaternion.identity);
    }
}
