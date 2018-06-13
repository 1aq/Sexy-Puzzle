using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


    public GameController gamer;
    private Vector3 MouseLocation;
    private RaycastHit2D hit;
    private Transform tr;
    private Vector3 EmptyLocation;
    private Vector3 temp;
    private bool IsSet;
    private bool IsViewer;
    private bool IsListen;
    private bool IsEnd;
    private string bestid;
    private int work; 
    private Text bushu;
    private Text best;
    private Image view;
    private Image buttonimg;
    private Image soundimg;
    public Sprite see;
    public Sprite no;
    public Sprite sound;
    public Sprite nosound;
    public AudioSource Bg;
    public AudioSource Do;
    public AudioSource Er;
    public AudioSource Win;
    public AudioSource Ch;

    // Use this for initialization
    void Start () {

        Bg.volume = PlayerPrefs.GetFloat("music", 0.5f);
        Do.volume = PlayerPrefs.GetFloat("sound", 0.5f);
        Er.volume = PlayerPrefs.GetFloat("sound", 0.5f);
        Win.volume = PlayerPrefs.GetFloat("sound", 0.5f);
        Ch.volume = PlayerPrefs.GetFloat("sound", 0.5f);


        IsSet = false;
        IsEnd = false;
        IsViewer = false;
        IsListen = true;
        work = 0;
        bushu = GameObject.Find("Canvas/Bushu").GetComponent<Text>();
        best = GameObject.Find("Canvas/Best").GetComponent<Text>();
        view = GameObject.Find("Canvas/Picture").GetComponent<Image>();
        buttonimg = GameObject.Find("Canvas/View").GetComponent<Image>();
        soundimg = GameObject.Find("Canvas/Sounds").GetComponent<Image>();

        bestid = "best" + DataBox.Data.levelNo;
        best.text = "最佳步数：" + PlayerPrefs.GetInt(bestid, 999);
    }
	
	// Update is called once per frame
	void Update () {
        if (!gamer.OnPrepare && !gamer.IsFinish && !IsViewer)
        {
            if (!IsSet)
            {
                EmptyLocation = new Vector3(2 * gamer.kl - 3, 2 - 2 * gamer.kh, 0);
                IsSet = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                MouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(new Vector2(MouseLocation.x, MouseLocation.y), new Vector2(MouseLocation.x, MouseLocation.y), 0.1f);
                if (hit.collider != null)
                {
                    tr = hit.collider.GetComponent<Transform>();
                    if (System.Math.Pow(tr.position.x - EmptyLocation.x, 2) + System.Math.Pow(tr.position.y - EmptyLocation.y, 2) == 4)
                    {
                        Do.Play();
                        temp = EmptyLocation;
                        EmptyLocation = tr.position;
                        tr.position = temp;
                        work++;
                        bushu.text = "当前步数：" + work;
                    }
                    else
                    {
                        Er.Play();
                    }
                }
            }

        }
        if (gamer.IsFinish && !IsEnd)
        {
            Win.Play();
            PlayerPrefs.SetInt(bestid, PlayerPrefs.GetInt(bestid, 999) > work ? work : PlayerPrefs.GetInt(bestid, 999));
            best.text = "最佳步数：" + PlayerPrefs.GetInt(bestid, 999);
            IsEnd = true;
            
        }
    }

    public void ToReset()
    {
        if (!IsViewer)
        {
            Ch.Play();
            gamer.ReSet();
            work = 0;
            bushu.text = "当前步数：" + work;
            IsSet = false;
        }
    }

    public void ToView()
    {
        Ch.Play();
        if (!IsViewer)
        {
            view.enabled = true;
            IsViewer = true;
            buttonimg.sprite = no;
        }
        else
        {
            view.enabled = false;
            IsViewer = false;
            buttonimg.sprite = see;
        }

    }

    public void ToListen()
    {
        if (IsListen)
        {
            IsListen = false;
            Bg.volume = 0;
            Ch.volume = 0;
            Do.volume = 0;
            Er.volume = 0;
            Win.volume = 0;
            soundimg.sprite = nosound;
        }
        else
        {
            IsListen = true;
            Bg.volume = PlayerPrefs.GetFloat("music", 0.5f); 
            Ch.volume = PlayerPrefs.GetFloat("sound", 0.5f);
            Do.volume = PlayerPrefs.GetFloat("sound", 0.5f);
            Er.volume = PlayerPrefs.GetFloat("sound", 0.5f);
            Win.volume = PlayerPrefs.GetFloat("sound", 0.5f);
            soundimg.sprite = sound;
        }
    }

    public void ToReturn()
    {
        DataBox.Data.sceneNo = 6;
        SceneManager.LoadScene(2);
    }

}
