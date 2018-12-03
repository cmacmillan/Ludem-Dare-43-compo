using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemiesOnEnter : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        coll.gameObject.layer = 10;//enemy layer
    }
}
