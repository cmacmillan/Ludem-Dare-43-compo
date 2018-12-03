using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBomb : Bullet
{
    public GameObject blackHolePrefab;
    public Manager gameManager;
    public override void HitCharacter(Character c)
    {
        //do nothing
    }
    public override void DestroyBullet()
    {
        var blackHole = Instantiate(blackHolePrefab);
        var blackholescript = blackHole.GetComponent<BlackHoleBullet>();
        blackholescript.gameManager = gameManager;
        gameManager.gravityWellPosition = this.transform.position;
        blackHole.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }
}
