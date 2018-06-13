using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadController : MonoBehaviour {

    public AudioSource ch;
    public Text number;
    public Transform tr;
    private AsyncOperation asy;
    private float process;
    private float ml;
    

	// Use this for initialization
	void Start () {
        ch.volume = PlayerPrefs.GetFloat("sound", 0.5f);
        StartCoroutine(loadScene());
        tr.position = new Vector3(0, 0.8f, -1);
        ml = 0;
	}
	
	// Update is called once per frame
	void Update () {
        process = asy.progress;
        if (asy.progress >= 0.9f)
        {
            process = 1.0f;
        }
        if (process >= ml)
        {
            ml += (1 / 30.0f);
            if (ml > 1)
                ml = 1;
            number.text = ((int)(ml * 100)).ToString();
            tr.position = new Vector3(1.3f * ml, 0.8f, -1);
        }
        if (ml == 1)
        {
            asy.allowSceneActivation = true;
        }
    }

    IEnumerator loadScene()
    {
        asy = SceneManager.LoadSceneAsync(DataBox.Data.sceneNo);
        asy.allowSceneActivation = false;
        yield return asy;
    }
}
