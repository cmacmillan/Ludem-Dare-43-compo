using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemicolonCooldown : MonoBehaviour
{
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
            if (gameManager.ship.blueBombTimeCounter > gameManager.ship.blueBombRateOfFire)
            {
                text.text = ";";
            }
            else
            {
                text.text = Mathf.CeilToInt(gameManager.ship.blueBombRateOfFire - gameManager.ship.blueBombTimeCounter) + "";
            }
        }
    }

}
