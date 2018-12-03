using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBullet : Bullet
{
    public Manager gameManager;
    public override void HitCharacter(Character c)
    {
        if (c.GetType() == typeof(shipcontroller))
        {
            var ship = (shipcontroller)c;
            if (ship != null && ship.sensor != null)
            {
                if (ship.sensor.iFrameTimeCounter > ship.sensor.iFrameTime)
                {
                    c.health -= damage;
                }
            }
        }
        else
        {
            c.health -= damage;
        }
    }
    public override void DestroyBullet()
    {
        gameManager.gravityWellPosition = null;
        Destroy(this.gameObject);
    }
}
