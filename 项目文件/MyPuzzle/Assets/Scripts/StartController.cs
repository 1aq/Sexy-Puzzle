using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour {

    public AudioSource enter;
    public AudioSource click;

    private Vector3 MouseLocation;
    private RaycastHit2D hit;
    private Renderer past;
    private Transform tri;

    // Use this for initialization
    void Start () {
        enter.volume = PlayerPrefs.GetFloat("sound", 0.5f);
        click.volume = PlayerPrefs.GetFloat("sound", 0.5f);
        if (DataBox.Data.startsound)
            click.Play();
        past = GameObject.Find("kaishi").GetComponent<Renderer>();
        tri = GameObject.Find("choose").GetComponent<Transform>();
        past.material.color = new Color(225 / 255.0f, 152 / 255.0f, 180 / 255.0f, 1);
    }
	
	// Update is called once per frame
	void Update () {
        MouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(new Vector2(MouseLocation.x, MouseLocation.y), new Vector2(MouseLocation.x, MouseLocation.y), 0.1f);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<Renderer>() != past)
            {
                enter.Play();
                past.material.color = new Color(1, 1, 1, 1);
                past = hit.collider.GetComponent<Renderer>();
                past.material.color = new Color(225 / 255.0f, 152 / 255.0f, 180 / 255.0f, 1);
                tri.position = new Vector3(-1.7f, past.transform.position.y - 0.05f, 0);
            }

            if (Input.GetMouseButtonDown(0))
            {

                DataBox.Data.startsound = true;
                switch (past.name)
                {
                    case "kaishi": ToStart(); break;
                    case "jilu": ToRecord(); break;
                    case "shezhi": ToSetting(); break;
                    case "tuichu": ToExit(); break;
                }
            }
        }
    }

    public void ToStart()
    {
        DataBox.Data.sceneNo = 6;
        SceneManager.LoadScene(2);
    }

    public void ToRecord()
    {
        SceneManager.LoadScene(4);
    }

    public void ToSetting()
    {
        SceneManager.LoadScene(3);
    }

    public void ToExit()
    {
        Application.Quit();
    }
}
