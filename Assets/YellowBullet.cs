using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullet : Bullet
{
    public override void HitCharacter(Character c)
    {
        c.health -= damage;
    }
    public float innerRadius;
    public float outerRadius;
    private CircleCollider2D circle;
    public void Start()
    {
        base.Start();
        circle = GetComponent<CircleCollider2D>();
    }
    public void Update()
    {
        base.Update();
        circle.radius = Mathf.Lerp(innerRadius, outerRadius, lifetimeCounter / lifetime);
    }
}
