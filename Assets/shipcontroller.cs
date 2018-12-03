using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipcontroller : Character
{

    // Use this for initialization
    public float speed = 1.0f;
    public float maxSpeed = 10.0f;
    public ParticleSystem exhaust;
    public float rotationSpeed = 1.0f;
    public Rigidbody2D rigid;
    public Transform leftBasicBarrel;
    public Transform rightBasicBarrel;
    public Transform yellowRingBarrel;
    public Transform blueBarrel;
    public GameObject basicBullet;
    public GameObject greenLazer;
    public GameObject yellowRing;
    public GameObject blueBomb;
    public Manager gameManager;
    public float blueBombRateOfFire = 1.0f;
    public float blueBombTimeCounter = 0.0f;
    public float yellowRingRateOfFire = 1.0f;
    public float yellowRingTimeCounter = 0.0f;
    public float greenLazerTimeCounter = 0.0f;
    public float greenLazerRateOfFire = 1.0f;
    public Transform greenLazerBarrel;
    public float basicBulletSpeed = 1.0f;
    public float basicRateOfFire = 1;
    public float basicBulletTimeCounter = 0;
    public float basicBulletNoise = 1.0f;
    public float basicBulletAngular = 50.0f;
    public AudioSource engineNoise;

    public DamageShip sensor;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        basicBulletTimeCounter = basicRateOfFire;
        yellowRingTimeCounter = yellowRingRateOfFire;
        blueBombTimeCounter = blueBombRateOfFire;
        greenLazerTimeCounter = greenLazerRateOfFire;
        //deathParticleZOffset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        basicBulletTimeCounter += Time.deltaTime;
        greenLazerTimeCounter += Time.deltaTime;
        yellowRingTimeCounter += Time.deltaTime;
        blueBombTimeCounter += Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            if (gameManager.disabledKeys.Contains(KeyCode.W))
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    badButtonPress.Play();
                }
            }
            else
            {
                engineNoise.volume = .25f;
                exhaust.emissionRate = 150;
                rigid.AddForce(this.transform.up * speed * Time.deltaTime);
                if (rigid.velocity.magnitude > maxSpeed)
                {
                    rigid.velocity = rigid.velocity.normalized * maxSpeed;
                }
            }
        }
        else
        {
            engineNoise.volume = 0.0f;
            exhaust.emissionRate = 0;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (gameManager.disabledKeys.Contains(KeyCode.A))
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    badButtonPress.Play();
                }
            }
            else
            {
                this.transform.Rotate(this.transform.forward, rotationSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (gameManager.disabledKeys.Contains(KeyCode.D))
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    badButtonPress.Play();
                }
            }
            else
            {
                this.transform.Rotate(this.transform.forward, -rotationSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.K))
        {
            if (gameManager.disabledKeys.Contains(KeyCode.K))
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    badButtonPress.Play();
                }
            }
            else
            {
                if (basicBulletTimeCounter > basicRateOfFire)
                {
                    spawnBulletAtPosition(leftBasicBarrel.position);
                    spawnBulletAtPosition(rightBasicBarrel.position);
                    basicBulletTimeCounter = 0;
                }
            }
        }

        if (Input.GetKey(KeyCode.L))
        {
            if (gameManager.disabledKeys.Contains(KeyCode.L))
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    badButtonPress.Play();
                }
            }
            else
            {
                if (greenLazerTimeCounter > greenLazerRateOfFire)
                {
                    greenLazerTimeCounter = 0;
                    spawnGreenLazerAtPosition(greenLazerBarrel.position);
                }
            }
        }

        if (Input.GetKey(KeyCode.J))
        {
            if (gameManager.disabledKeys.Contains(KeyCode.J))
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    badButtonPress.Play();
                }
            }
            else
            {
                if (yellowRingTimeCounter > yellowRingRateOfFire)
                {
                    yellowRingTimeCounter = 0;
                    spawnYellowRingAtPosition(yellowRingBarrel.position);
                }
            }
        }

        if (Input.GetKey(KeyCode.Semicolon))
        {
            if (gameManager.disabledKeys.Contains(KeyCode.Semicolon))
            {
                if (Input.GetKeyDown(KeyCode.Semicolon))
                {
                    badButtonPress.Play();
                }
            }
            else
            {
                if (blueBombTimeCounter > blueBombRateOfFire)
                {
                    blueBombTimeCounter = 0;
                    spawnBlueBombAtPosition(blueBarrel.transform.position);
                }
            }
        }
    }
    public AudioSource badButtonPress;
    public float initialBombVelocity;
    void spawnBlueBombAtPosition(Vector3 pos)
    {
        var bomb = Instantiate(blueBomb);
        bomb.transform.position = pos;
        var bombScript = bomb.GetComponent<BlackBomb>();
        bombScript.gameManager = gameManager;
        var bombRigid = bombScript.GetComponent<Rigidbody2D>();
        bombRigid.AddForce((Vector2)this.transform.up * initialBombVelocity);
        bombRigid.velocity += rigid.velocity * 2.5f;
    }
    public void spawnYellowRingAtPosition(Vector3 pos)
    {
        var ring = Instantiate(yellowRing);
        ring.transform.position = pos;
    }
    void spawnGreenLazerAtPosition(Vector3 pos)
    {
        var lazer = Instantiate(greenLazer);
        lazer.transform.position = pos;
        lazer.transform.rotation *= this.transform.rotation;
    }

    public override void DestroyThisCharacter()
    {
        Destroy(this.gameObject);
    }
    void spawnBulletAtPosition(Vector3 pos)
    {
        var bullet = Instantiate(basicBullet);
        var rigid = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.position = pos;
        var noise = ((Vector2)Random.insideUnitSphere.normalized) * basicBulletNoise;
        rigid.velocity = this.rigid.velocity + ((Vector2)(this.transform.up) + noise).normalized * basicBulletSpeed;
        rigid.angularVelocity = Random.Range(-basicBulletAngular, basicBulletAngular);
        bullet.transform.rotation *= Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
    }
}
