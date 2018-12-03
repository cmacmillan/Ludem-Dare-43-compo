using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{

    // Use this for initialization
    public float timeToDeath;
    private float deathCounter;
    void Start()
    {
        deathCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        deathCounter += Time.deltaTime;
        if (timeToDeath < deathCounter)
        {
            Destroy(this.gameObject);
        }
    }
}
