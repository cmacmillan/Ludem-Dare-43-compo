using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    public bool isEnemy = true;
    public int health = 1;
    public GameObject explosionParticle;
    public GameObject[] lines;

    public float deathParticleZOffset = 6;
    public bool isAlive = true;

    public void Die()
    {
        if (isAlive)
        {
            foreach (var i in lines)
            {
                i.transform.parent = null;
                var rigid = i.AddComponent<Rigidbody2D>();
                rigid.gravityScale = 0;
                rigid.drag = 3;
                i.transform.position += new Vector3(0, 0, deathParticleZOffset);
                rigid.velocity = Random.insideUnitCircle.normalized * 3;
                var destroyAfterSeconds = i.AddComponent<DestroyShrap>();
                destroyAfterSeconds.timeToDeath = 3.0f;
            }
            var explosion = Instantiate(explosionParticle);
            explosion.transform.position = this.transform.position;
            isAlive = false;
            DestroyThisCharacter();
        }
    }
    public abstract void DestroyThisCharacter();
    public virtual void Update()
    {
        if (health <= 0)
        {
            /*if (typeof(shipcontroller) == this.GetType())
            {
                var ship = (shipcontroller)this;
                if (ship.respawnImmunityCounter > ship.respawnImmunityTime)
                {
                    Die();
                }
            }
            else
            {
                Die();
            }*/
            Die();
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        var bullet = coll.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.HitCharacter(this);
        }
    }
}
