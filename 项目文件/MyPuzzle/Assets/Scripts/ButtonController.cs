using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public AudioSource ch;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToPlay()
    {
        ch.volume = PlayerPrefs.GetFloat("sound", 0.5f);
        ch.Play();
    }
}
