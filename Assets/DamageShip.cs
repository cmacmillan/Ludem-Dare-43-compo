using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShip : MonoBehaviour
{

    // Use this for initialization
    public shipcontroller ship;

    // Update is called once per frame
    void Update()
    {
        iFrameTimeCounter += Time.deltaTime;
    }
    public float iFrameTime = 1.0f;
    public float iFrameTimeCounter = 0.0f;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (iFrameTimeCounter > iFrameTime)
        {
            ship.health -= 1;
            iFrameTimeCounter = 0;
        }
    }
}
