using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JCooldown : MonoBehaviour
{

    // Use this for initialization
    public Manager gameManager;
    private TextMesh text;
    void Start()
    {
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.ship != null)
        {
            if (gameManager.ship.yellowRingTimeCounter > gameManager.ship.yellowRingRateOfFire)
            {
                text.text = "J";
            }
            else
            {
                text.text = Mathf.CeilToInt(gameManager.ship.yellowRingRateOfFire - gameManager.ship.yellowRingTimeCounter) + "";
            }
        }
    }
}
