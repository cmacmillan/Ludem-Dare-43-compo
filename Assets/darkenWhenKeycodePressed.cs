using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkenWhenKeycodePressed : MonoBehaviour
{

    // Use this for initialization
    public KeyCode key;
    public float darkenFactor;
    private TextMesh text;
    private Color textBaseColor;
    void Start()
    {
        text = GetComponent<TextMesh>();
        textBaseColor = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key))
        {
            text.color = textBaseColor * darkenFactor;
        }
        else
        {
            text.color = textBaseColor;
        }
    }
}
