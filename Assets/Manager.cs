using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{

    // Use this for initialization
    public shipcontroller ship;
    public trackShip camTrackShip;
    public GameObject shipPrefab;
    public List<string> waves;
    public int currentWave;
    public float timeBetweenWaves = 15.0f;
    public float waveTimeCounter = 0.0f;
    public GameObject shipRespawnParticles;
    private Vector3 shipPosition;
    public List<KeyCode> disabledKeys;
    public darkenWhenKeycodePressed[] buttons;
    private Quaternion shipRotation;

    public List<Enemy> enemies;
    public GameObject whiteEnemy;
    public TextMesh infoText;
    public GameObject redEnemy;
    public GameObject greenEnemy;

    string getWaveText()
    {
        var split = waves[currentWave].Split(',');
        return split[0] + " cyan(s) " + split[1] + " red(s) " + split[2] + " green(s)";
    }
    void spawnNextWave()
    {
        infoText.text = "Wave " + (currentWave + 1) + "\n" + getWaveText();
        string waveInfo;
        if (currentWave >= waves.Count)
        {
            waveInfo = waves[waves.Count - 1];
        }
        else
        {
            waveInfo = waves[currentWave];
        }
        var split = waveInfo.Split(',');
        for (int i = 0; i < int.Parse(split[0]); i++)
        {
            spawnEnemy(whiteEnemy);
        }
        for (int i = 0; i < int.Parse(split[1]); i++)
        {
            {
                spawnEnemy(redEnemy);
            }
        }
        for (int i = 0; i < int.Parse(split[2]); i++)
        {
            {
                spawnEnemy(greenEnemy);
            }
        }
        currentWave++;
    }
    void Start()
    {
        currentWave = 0;
        waveTimeCounter = timeBetweenWaves;
        isInSacMode = false;
        disabledKeys = new List<KeyCode>();
        enemies = new List<Enemy>();
        ship.badButtonPress = GetComponent<AudioSource>();
        gravityWellPosition = null;
        //gravityWellPosition = blackhole.transform.position;
    }
    //public Transform blackhole;
    public Vector3? gravityWellPosition;
    public float gravityWellForce;
    public Transform centerOfPlayArea;
    public float radiusOfPlayArea;
    public float widthOfPlayArea;

    void spawnEnemy(GameObject enemyPrefab)
    {
        var enemyObject = Instantiate(enemyPrefab);
        var enemyScript = enemyObject.GetComponent<Enemy>();
        enemyScript.gameManager = this;
        enemies.Add(enemyScript);
        var spawnPos = getSpawnPosition();
        enemyObject.transform.position = spawnPos;
        enemyObject.transform.rotation = getSpawnRotation(spawnPos);
    }
    /* void spawnWhiteAtPosition(Vector3 pos, Vector3 center)
    {
        var enemy = Instantiate(whiteEnemy);
        var enemyScript = enemy.GetComponent<WhiteEnemy>();
        enemyScript.gameManager = this;
        enemies.Add(enemyScript);
        enemy.transform.position = pos;
        var up = pos - center;
        enemy.transform.rotation = Quaternion.LookRotation(up, Vector3.forward);
	}*/
    Vector2 getSpawnPosition()
    {
        return Random.insideUnitCircle.normalized * radiusOfPlayArea + (Vector2)centerOfPlayArea.position;
    }
    Quaternion getSpawnRotation(Vector2 spawnPosition)
    {
        var xRand = Random.Range(-widthOfPlayArea / 2, widthOfPlayArea / 2);
        var yRand = Random.Range(-widthOfPlayArea / 2, widthOfPlayArea / 2);
        var pos = new Vector2(xRand, yRand) + (Vector2)centerOfPlayArea.position;
        return Quaternion.LookRotation(Vector3.forward, pos - spawnPosition);
    }
    void ApplyGravityWell()
    {
        if (gravityWellPosition != null)
        {
            foreach (var i in enemies)
            {
                if (i.gameObject.layer == 10)
                {
                    var direction = (gravityWellPosition - i.transform.position).Value.normalized;
                    var distanceSquared = Mathf.Pow(Vector2.Distance((Vector2)gravityWellPosition, (Vector2)i.transform.position), 2);
                    i.rigid.AddForce((Vector2)direction * Time.deltaTime * gravityWellForce / distanceSquared);
                }
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ApplyGravityWell();
    }

    private bool isInSacMode = false;
    private List<GameObject> lightXList;
    public GameObject lightx;
    public GameObject darkx;
    public Vector3 xOffset;
    void showLightX()
    {
        lightXList = new List<GameObject>();
        foreach (var i in buttons)
        {
            var lx = Instantiate(lightx, xOffset + i.transform.position, Quaternion.identity);
            lightXList.Add(lx);
        }
    }
    void hideLightX()
    {
        foreach (var i in lightXList)
        {
            Destroy(i);
        }
    }
    private KeyCode? currentlySelectedKey;
    private float timeSinceDeath;
    public float timeBeforeInputs = 1.0f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        timeSinceDeath += Time.deltaTime;
        if (ship != null)
        {
            shipRotation = ship.transform.rotation;
            shipPosition = ship.transform.position;
            if (waveTimeCounter > timeBetweenWaves)
            {
                spawnNextWave();
                waveTimeCounter = 0;
            }
            waveTimeCounter += Time.deltaTime;
        }
        else
        {
            if (isInSacMode)
            {
                if (timeSinceDeath > timeBeforeInputs)
                {
                    foreach (var i in buttons)
                    {
                        if (Input.GetKeyDown(i.key) && !disabledKeys.Contains(i.key))
                        {
                            if (i.key == currentlySelectedKey)
                            {
                                var particles = Instantiate(shipRespawnParticles, shipPosition, shipRotation);
                                var shipGameObj = Instantiate(shipPrefab, shipPosition, shipRotation);
                                ship = shipGameObj.GetComponent<shipcontroller>();
                                ship.badButtonPress = GetComponent<AudioSource>();
                                ship.gameManager = this;
                                disabledKeys.Add(i.key);
                                isInSacMode = false;
                                var dark = Instantiate(darkx, i.transform.position + xOffset, Quaternion.identity);
                                dark.transform.parent = i.transform;
                                camTrackShip.ship = ship.transform;
                                hideLightX();
                                infoText.text = "Wave: " + currentWave + " " + getWaveText();
                            }
                            else
                            {
                                currentlySelectedKey = i.key;
                                infoText.text = "Press " + i.key.ToString() + " again \nto sacrifice\nWave: " + currentWave;
                            }
                        }

                    }
                }
            }
            else
            {
                timeSinceDeath = 0;
                infoText.text = "DEATH! PICK a weapon to DESTROY,\nor press R to restart the game\nWave: " + currentWave;
                currentlySelectedKey = null;
                showLightX();
                isInSacMode = true;
            }
        }
    }
}
