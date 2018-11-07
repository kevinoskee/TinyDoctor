
using UnityEngine;
using UnityEngine.AI;

public class Virus : MonoBehaviour {

    public float health = 50f;
    public Bullet bullet;
    public GameObject HitEffect;
    public GameObject DestroyEffect;
    public float lookRadius;
    Transform boss;
    Transform tempLoc;
    Transform player;
    NavMeshAgent virus;

    bool found = false;
    bool returned = false;
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Start()
    {
        int index = Random.Range(0, TempLocManager.instance.tempLoc.Length);
        player = PlayerManager.instance.player.transform;
        boss = BossManager.instance.boss.transform;
        tempLoc = TempLocManager.instance.tempLoc[index].transform;
        virus = GetComponent<NavMeshAgent>();
        FindPlayer();
    }

    void FindPlayer()
    {
        virus.SetDestination(tempLoc.position);
        returned = false;
    }

    void GoBack()
    {
        virus.SetDestination(boss.position);
        returned = true;
    }
    void GoToPlayer()
    { 
        virus.SetDestination(player.position);
        found = true;
        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation,
                             Quaternion.LookRotation(direction), 0.1f);
    }

    private void Update()
    {
        if (!found)
        {
            if (!virus.pathPending)
            {
                if (virus.remainingDistance <= 2)
                {
                    
                        if (returned)
                            FindPlayer();
                        else
                            GoBack();
                    
                }
            }
        }

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= lookRadius)
        {
            GoToPlayer();
        }



    }

    void Die()
    {
        Destroy(gameObject);
        GameObject destroyEffect;
        destroyEffect = Instantiate(DestroyEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        Destroy(destroyEffect, 2f);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }


}
