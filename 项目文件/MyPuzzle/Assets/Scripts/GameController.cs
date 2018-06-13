using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour {
    
    public Renderer[] R;
    public SpriteRenderer vag;
    public Image vie;
    public Text txt;
    

    public Sprite[] puz1;
    public Sprite[] puz2;
    public Sprite[] puz3;
    public Sprite[] puz4;
    public Sprite[] puz5;
    public Sprite[] puz6;
    public Sprite[] puz7;
    public Sprite[] puz8;
    public Sprite[] puz9;

    public Sprite[] vies;
    public Sprite[] vags;
    

    private List<Sprite[]> puzs;
    private Transform[] T;
    private SpriteRenderer[] SR;

    private Vector2[] V;

    private bool ch;
    private int[,] a;
    private int h, l, n;
    private int picNo;
    private int i;
    
    private string countid;

    public bool OnPrepare;
    public bool IsFinish;
    public bool le;
    public int kh, kl;

    

    private Text win;

    // Use this for initialization
    void Start () {
        

        h = DataBox.Data.levelH;
        l = DataBox.Data.levelL;
        n = DataBox.Data.levelE;
        picNo = DataBox.Data.levelNo - 1;

        puzs = new List<Sprite[]>();
        puzs.Add(puz1);
        puzs.Add(puz2);
        puzs.Add(puz3);
        puzs.Add(puz4);
        puzs.Add(puz5);
        puzs.Add(puz6);
        puzs.Add(puz7);
        puzs.Add(puz8);
        puzs.Add(puz9);

        txt.text = string.Format("图片 " + (picNo + 1));

        OnPrepare = true;
        ch = true;
        IsFinish = false;
        a = new int[h, l];
        le = false;

        
        countid = "count" + DataBox.Data.levelNo;

        win = GameObject.Find("Canvas/Complete").GetComponent<Text>();

        

        T = new Transform[h*l];
        SR = new SpriteRenderer[h * l];
        V = new Vector2[h * l];

        for (i = 0; i < h*l; i++)
        {
            T[i] = R[i].GetComponent<Transform>();
            SR[i] = R[i].GetComponent<SpriteRenderer>();
        }

        for (i = 0; i < h * l; i++)
        {
            SR[i].sprite = puzs[picNo][i];
        }

        vag.sprite = vags[picNo];
        vie.sprite = vies[picNo];

        int vy = 2 * (h / 2), vx = 1 - l;
        if (n == 11)
        {
            V[0].x = vx + 2 * (l - 1);
            V[0].y = vy - 2 * (h - 1);
        }
        for (i = 1; i < h * l; i++)
        {
            if (i == n+1)
            {
                V[0].x = vx;
                V[0].y = vy;
                vx = vx + 2;
            }
            if (vx == l + 1)
            {
                vx = 1 - l;
                vy = vy - 2;
            }
            V[i].x = vx;
            V[i].y = vy;
            vx = vx + 2;
            if (vx == l + 1)
            {
                vx = 1 - l;
                vy = vy - 2;
            }
        }

        for (i = 0; i < h * l; i++)
        {
            T[i].position = new Vector3(V[i].x, V[i].y,T[i].position.z);
        }

        R[0].material.color = new Color(R[0].material.color.r, R[0].material.color.b, R[0].material.color.g, 0);

        MakePuzzle();
        
        
    }
	
	// Update is called once per frame
	void Update () {
        
        if (OnPrepare)
        {
            if (le)
            {
                if (ch)
                {
                    ReSet();
                    ch = false;
                }

                for (i = 1; i < h * l; i++)
                    R[i].material.color += new Color(0, 0, 0, 0.02f);
                if (R[1].material.color.a >= 1)
                    OnPrepare = false;
            }
            else
            {
                for (i = 1; i < h * l; i++)
                    R[i].material.color -= new Color(0, 0, 0, 0.03f);
                if (R[1].material.color.a <= 0)
                    le = true;
            }
        }
        else
        {
            
            if (!IsFinish)
            {
                
                bool panduan = true;
                for (i = 1; i < h * l; i++)
                {
                    if (T[i].position.x != V[i].x || T[i].position.y != V[i].y)
                    {
                        panduan = false;
                    }
                }
                if (panduan)
                {
                    IsFinish = true;
                    win.enabled = true;
                    PlayerPrefs.SetInt(countid, PlayerPrefs.GetInt(countid, 0) + 1);
                }

            }
            if (IsFinish)
                if (R[0].material.color.a < 1)
                    R[0].material.color += new Color(0, 0, 0, (float)0.02);

        }
	}

    private void MakePuzzle()
    {

        kh = n / l;
        kl = n % l;
        int i = 0;
        int last = 0, next;
        int o = 1, p, q;
        int[,] b = new int[h, l];
        for (p = 0; p < h; p++)
        {
            for (q = 0; q < l; q++)
            {
                if (p != kh || q != kl)
                {
                    a[p, q] = o;
                    o++;
                }
            }
        }
        while (i < 12)
        {
            next = Random.Range(1, 5);
            if (last + next != 5)
            {
                switch (next)
                {
                    case 1:
                        if (kl != l-1)
                        {
                            a[kh, kl] = a[kh, kl + 1];
                            kl++;
                            last = next;
                            if (b[kh, kl] == 0)
                            {
                                b[kh, kl] = 1;
                                i++;
                            }
                        }
                        break;
                    case 2:
                        if (kh != h-1)
                        {
                            a[kh, kl] = a[kh + 1, kl];
                            kh++;
                            last = next;
                            if (b[kh, kl] == 0)
                            {
                                b[kh, kl] = 1;
                                i++;
                            }
                        }
                        break;
                    case 3:
                        if (kh != 0)
                        {
                            a[kh, kl] = a[kh - 1, kl];
                            kh--;
                            last = next;
                            if (b[kh, kl] == 0)
                            {
                                b[kh, kl] = 1;
                                i++;
                            }
                        }
                        break;
                    case 4:
                        if (kl != 0)
                        {
                            a[kh, kl] = a[kh, kl - 1];
                            kl--;
                            last = next;
                            if (b[kh, kl] == 0)
                            {
                                b[kh, kl] = 1;
                                i++;
                            }
                        }
                        break;
                }
            }
        }
        a[kh, kl] = 0;
        
    }

    public void ReSet()
    {
        int p, q;
        IsFinish = false;
        win.enabled = false;
        for (p = 0; p < h; p++)
        {
            for (q = 0; q < l; q++)
            {
                if (a[p, q] != 0)
                    T[a[p, q]].position = new Vector3(2 * q + 1-l, 2*(h/2) - 2 * p, 0);
            }
        }
    }
    
}
