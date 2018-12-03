using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lcooldown : MonoBehaviour
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
            if (gameManager.ship.greenLazerTimeCounter > gameManager.ship.greenLazerRateOfFire)
            {
                text.text = "L";
            }
            else
            {
                text.text = Mathf.CeilToInt(gameManager.ship.greenLazerRateOfFire - gameManager.ship.greenLazerTimeCounter) + "";
            }
        }
    }
}
