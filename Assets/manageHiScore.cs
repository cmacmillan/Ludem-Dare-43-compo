using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageHiScore : MonoBehaviour
{

    // Use this for initialization
    public TextMesh text;
    public Manager gameManager;
    private int hiScore = 1;
    void Start()
    {
        text = GetComponent<TextMesh>();
        if (!PlayerPrefs.HasKey("HiScore"))
        {
            PlayerPrefs.SetFloat("HiScore", 1);
            hiScore = 1;
        }
        else
        {
            hiScore = PlayerPrefs.GetInt("HiScore");
            text.text = "Highest Wave Reached: " + hiScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentWave > hiScore)
        {
            hiScore = gameManager.currentWave;
            text.text = "Highest Wave Reached: " + hiScore;
            PlayerPrefs.SetInt("HiScore", hiScore);
        }
    }
}
