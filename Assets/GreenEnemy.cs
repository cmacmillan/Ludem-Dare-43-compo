using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : Enemy
{
    public SpriteRenderer[] sprites;
    private int healthLastFrame;
    public bool isFlickering;
    private Color startColor;
    public Transform[] projectilePoints;

    public float firerate;
    public float fireCounter;
    public GameObject whiteEnemy;
    public Transform center;
    private const float startVelocity = 2.0f;
    public override void Start()
    {
        base.Start();
        rigid.velocity = this.transform.up * startVelocity;
        startColor = sprites[0].color;
        isFlickering = false;
        healthLastFrame = health;
    }
    public void Fire()
    {
        foreach (var i in projectilePoints)
        {
            var enemy = Instantiate(whiteEnemy);
            var enemyScript = enemy.GetComponent<WhiteEnemy>();
            enemyScript.gameManager = this.gameManager;
            this.gameManager.enemies.Add(enemyScript);
            enemy.transform.position = i.position;
            Vector2 up = i.position - center.position;
            enemy.transform.rotation = Quaternion.LookRotation(Vector3.forward, up);
            enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0);
        }
    }
    public override void Update()
    {
        base.Update();
        if (this.gameObject.layer == 10)//jam jam, lol <-- weed lol (tried to type game jam)
        {
            fireCounter += Time.deltaTime;
            if (fireCounter > firerate)
            {
                fireCounter = 0;
                Fire();
            }
        }
        if (isFlickering && health == healthLastFrame)
        {
            isFlickering = false;
            foreach (var i in sprites)
            {
                i.color = startColor;
            }
        }
        else if (health != healthLastFrame)
        {
            if (!isFlickering)
            {
                isFlickering = true;
                foreach (var i in sprites)
                {
                    i.color = startColor * .7f;
                }
            }
        }
        healthLastFrame = health;
    }
}
