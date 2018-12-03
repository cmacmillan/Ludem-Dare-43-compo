using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBullet : Bullet
{
    public override void HitCharacter(Character c)
    {
        c.health -= damage;
    }

}
