using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ParticleSystem impactFX;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impactFX, transform.position, Quaternion.identity);

        if(collision.gameObject.tag == "PlayerTank")
        {
            collision.gameObject.GetComponent<TankHealth>().takeDamage(20);
        }

        if (collision.gameObject.tag == "EnemyTank")
        {
            collision.gameObject.GetComponent<EnemyTankHealth>().takeDamage(20);
        }
        Destroy(this.gameObject);

        

    }
}
