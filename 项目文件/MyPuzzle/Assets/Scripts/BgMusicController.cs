using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicController : MonoBehaviour {

    private float voice;
    private static BgMusicController backMusic = null;

    public static BgMusicController BackMusic
    {
        get { return backMusic; }
    }



    // Use this for initialization
    void Start () {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("music",0.5f);
        voice = this.GetComponent<AudioSource>().volume;
    }

    private void Awake()
    {
        if (backMusic != null && backMusic != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            backMusic = this;
        }
        DontDestroyOnLoad(this.gameObject);
        
    }
    


    void Update () {
        if (DataBox.Data.sceneNo == 5)
        {
            voice = 0;
            this.GetComponent<AudioSource>().volume = voice;
        }
        else
        {
            if (voice != PlayerPrefs.GetFloat("music", 0.5f))
            {
                voice = PlayerPrefs.GetFloat("music", 0.5f) ;
                this.GetComponent<AudioSource>().volume = voice;
            }
        }
    }
}
