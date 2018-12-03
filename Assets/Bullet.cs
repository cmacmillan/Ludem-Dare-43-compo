using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{

    public int damage = 1;
    public float lifetime = 1.0f;
    protected float lifetimeCounter = 0.0f;
    // Use this for initialization
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        lifetimeCounter += Time.deltaTime;
        if (lifetimeCounter > lifetime)
        {
            DestroyBullet();
        }
    }

    public virtual void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    public abstract void HitCharacter(Character c);

    void OnTriggerEnter2D(Collider2D coll)
    {
        var character = coll.GetComponent<Character>();
        if (character)
        {
            HitCharacter(character);
        }
    }
}
