using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyText : MonoBehaviour
{

    // Use this for initialization
    public Material overlapMat;
    public Camera maincam;
    void Start()
    {

    }

    // Update is called once per frame
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        overlapMat.SetTexture("_UnderTex", maincam.targetTexture);
        Graphics.Blit(src, dest, overlapMat);
    }
}
