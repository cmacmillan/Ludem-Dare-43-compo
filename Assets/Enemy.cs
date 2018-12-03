using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{

    // Use this for initialization
    public Rigidbody2D rigid;
    public Manager gameManager;


    public virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public override void DestroyThisCharacter()
    {
        gameManager.enemies.Remove(this);
        Destroy(this.gameObject);
    }

}
