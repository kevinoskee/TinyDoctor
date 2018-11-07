using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public GameObject Bullet;

    void Start()
    {
        Physics.IgnoreCollision(Bullet.GetComponent<Collider>(), GetComponent<Collider>());
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
