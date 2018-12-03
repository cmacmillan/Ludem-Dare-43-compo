using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxStars : MonoBehaviour
{

    // Use this for initialization
    public Transform camera;
    private Vector3 startingPos;
    public float slideFactor = 1.0f;
    void Start()
    {
        startingPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = (camera.position - startingPos) * slideFactor;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, startingPos.z);
    }
}
