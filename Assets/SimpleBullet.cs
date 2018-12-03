using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : Bullet
{
    public override void HitCharacter(Character c)
    {
        c.health -= this.damage;
        DestroyBullet();
    }
}
