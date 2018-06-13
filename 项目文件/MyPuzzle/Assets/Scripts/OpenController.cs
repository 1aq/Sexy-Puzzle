using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenController : MonoBehaviour {


    public Renderer[] t;
    private int i,j;
    private int va;
    private bool isFull;

	// Use this for initialization
	void Start () {
        i = 0;
        if (PlayerPrefs.GetInt("windowsed", 3) < 10)
        {
            isFull = false;
            va = PlayerPrefs.GetInt("windowsed", 3);
        } else
        {
            isFull = true;
            va = PlayerPrefs.GetInt("windowsed", 3) - 10;
        }
        switch (va)
        {
            case 0: Screen.SetResolution(1920, 1080, isFull); break;
            case 1: Screen.SetResolution(1600, 900, isFull); break;
            case 2: Screen.SetResolution(1280, 720, isFull); break;
            case 3: Screen.SetResolution(1024, 576, isFull); break;
            case 4: Screen.SetResolution(960, 540, isFull); break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(t[0].material.color.a);
        if (i <= 60)
        {
            for (j = 0; j < 6; j++)
                t[j].material.color = new Color(t[j].material.color.r, t[j].material.color.g, t[j].material.color.b, i / 60.0f);
            i++;
        }
        else if (i <= 240)
        {
            i++;
        }
        else if (i <= 300)
        {
            for (j = 0; j < 6; j++)
                t[j].material.color = new Color(t[j].material.color.r, t[j].material.color.g, t[j].material.color.b, (300 - i) / 60.0f);
            i++;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
	}
}
