
using UnityEngine;

public class GunControl : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public FireBtn FireBtn;

    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;


    public Camera fpsCam;
    public GameObject HitEffect;

    // Update is called once per frame
    void Update () {
        if (FireBtn.Pressed)
        {
            Shoot();
            FireBtn.Pressed = false;
        }
	}

 

    void Shoot()
    {
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

        //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
        //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
        Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

        //Retrieve the Rigidbody component from the instantiated Bullet and control it.
        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
        Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

        Destroy(Temporary_Bullet_Handler, 0.3f);



        //RaycastHit hit;
        //if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        //{
        //    if (hit.transform.name == "Target")
        //    {
        //        Target target = hit.transform.GetComponent<Target>();
        //        if (target != null)
        //        {
        //            GameObject impact = Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //            target.TakeDamage(damage);
        //            Destroy(impact, 2f);
        //        }
        //    }

        //}
    }
}
