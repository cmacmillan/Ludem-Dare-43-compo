using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackShip : MonoBehaviour
{

    // Use this for initialization
    public Transform ship;
    public Camera TextCamera;
    private Camera thiscam;
    void Start()
    {
        thiscam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ship != null)
        {
            this.transform.position = ship.transform.position - new Vector3(0, 0, 10);
        }
    }
}
