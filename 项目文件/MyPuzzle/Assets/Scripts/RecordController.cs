using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RecordController : MonoBehaviour {

    private int i;

    public AudioSource click;
    public Text[] w;
    public Text[] t;
    public Text a;
	// Use this for initialization
	void Start () {
        click.volume = PlayerPrefs.GetFloat("sound", 0.5f);
        click.Play();
        ToShow();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ToShow()
    {
        int count = 0;
        for (i = 0; i < 9; i++)
        {
            w[i].text = PlayerPrefs.GetInt(string.Format("count" + (i+1)), 0).ToString();
            t[i].text = PlayerPrefs.GetInt(string.Format("best" + (i+1)), 999).ToString();
            count += PlayerPrefs.GetInt(string.Format("count" + (i+1)), 0);
        }
        a.text = count.ToString();
    }

    public void ToReset()
    {
        PlayerPrefs.DeleteAll();
        ToShow();
    }

    public void ToReturn()
    {
        SceneManager.LoadScene(1);
    }
}
