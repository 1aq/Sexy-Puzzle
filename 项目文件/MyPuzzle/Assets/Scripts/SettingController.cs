using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingController : MonoBehaviour {

    public AudioSource click;
    public Text tmu;
    public Text tso;
    public Slider smu;
    public Slider sso;
    public Toggle wind;
    public Toggle full;
    public Dropdown dp;
    private float pmu;
    private float pso;
    private float nmu;
    private float nso;

	// Use this for initialization
	void Start () {
        click.volume = PlayerPrefs.GetFloat("sound", 0.5f);
        click.Play();

        pmu = PlayerPrefs.GetFloat("music", 0.5f);
        pso = PlayerPrefs.GetFloat("sound", 0.5f);
        nmu = PlayerPrefs.GetFloat("music", 0.5f);
        nso = PlayerPrefs.GetFloat("sound", 0.5f);

        smu.value = pmu;
        sso.value = pso;
        if (PlayerPrefs.GetInt("windowsed", 3) < 10)
        {
            wind.isOn = true;
            dp.value = PlayerPrefs.GetInt("windowsed", 3);
        }
        else
        {
            full.isOn = true;
            dp.value = PlayerPrefs.GetInt("windowsed", 3) - 10;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (smu.value != nmu)
        {
            tmu.text = ((int)(smu.value * 100)).ToString();
            nmu = smu.value;
            PlayerPrefs.SetFloat("music",nmu);
        }
        if (sso.value != nso)
        {
            tso.text = ((int)(sso.value * 100)).ToString();
            nso = sso.value;
            PlayerPrefs.SetFloat("sound", nso);
        }

    }

    void ChangeScreenMode()
    {

        if (wind.isOn)
        {
            PlayerPrefs.SetInt("windowsed", dp.value);
        }
        else
        {
            PlayerPrefs.SetInt("windowsed", dp.value+10);
        }
        switch (dp.value)
        {
            case 0: Screen.SetResolution(1920, 1080, full.isOn);break;
            case 1: Screen.SetResolution(1600, 900, full.isOn); break;
            case 2: Screen.SetResolution(1280, 720, full.isOn); break;
            case 3: Screen.SetResolution(1024, 576, full.isOn); break;
            case 4: Screen.SetResolution(960, 540, full.isOn); break;
        }


    }

    public void ToYes()
    {
        ChangeScreenMode();
        SceneManager.LoadScene(1);
    }

    public void ToNo()
    {
        PlayerPrefs.SetFloat("music", pmu);
        PlayerPrefs.SetFloat("sound", pso);
        SceneManager.LoadScene(1);
    }


    public void ToRset()
    {
        smu.value = 0.5f;
        sso.value = 0.5f;
        wind.isOn = true;
        dp.value = 3;
    }
}
