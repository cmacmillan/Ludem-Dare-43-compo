using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShrap : MonoBehaviour
{

    // Use this for initialization
    public float timeToDeath;
    private float deathCounter;
    void Start()
    {
        deathCounter = 0;
        isDying = false;
    }

    // Update is called once per frame
    private bool isDying = false;
    void Update()
    {
        deathCounter += Time.deltaTime;
        if (isDying)
        {
            Destroy(this.gameObject);
        }
        if (timeToDeath < deathCounter)
        {
            this.gameObject.layer = 15;
            isDying = true;
        }
    }
}
