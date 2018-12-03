using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script clears the rendertexture to black with alpha of 0 for one frame, then switches the camera to normal no-clear mode
public class ClearRTToBlackAlpha : MonoBehaviour
{

    // Use this for initialization
    private Camera thiscam;
    private int framecounter = 0;
    private int cullingMaskRef;
    void Start()
    {
        thiscam = GetComponent<Camera>();
        cullingMaskRef = thiscam.cullingMask;
        ClearScreen();
    }

    // Update is called once per frame
    [ContextMenu("Clear Screen")]
    void ClearScreen()
    {
        framecounter = 0;
        thiscam.clearFlags = CameraClearFlags.Color;
        thiscam.cullingMask = 0;
        thiscam.backgroundColor = new Color(0, 0, 0, 0);
    }
    void Update()
    {
        if (framecounter == 1)
        {
            thiscam.clearFlags = CameraClearFlags.Nothing;
            thiscam.cullingMask = cullingMaskRef;
        }
        framecounter++;
    }
}
