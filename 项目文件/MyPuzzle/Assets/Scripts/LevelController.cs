using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    public AudioSource enter;
    private Vector3 MouseLocation;
    private RaycastHit2D hit;
    private Collider2D past;
    private SpriteRenderer bg;
    
    
    public Sprite[] vague;
    public int[] emp;
    public int[] hang;
    public int[] lie;
    public Text picn;
    public Text comp;
    public Text best;
    public Image box;
    

    // Use this for initialization
    void Start() {

        enter.volume = PlayerPrefs.GetFloat("sound", 0.5f);
        
        bg = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(new Vector2(MouseLocation.x, MouseLocation.y), new Vector2(MouseLocation.x, MouseLocation.y), 0.1f);
        if (hit.collider != null)
        {
            if (past != hit.collider)
            {
                enter.Play();
                box.enabled = true;
                picn.text = "图片 " + hit.collider.name;
                comp.text = "完成次数：" + PlayerPrefs.GetInt(string.Format("count" + int.Parse(hit.collider.name)), 0);
                best.text = "最佳步数：" + PlayerPrefs.GetInt(string.Format("best" + int.Parse(hit.collider.name)), 999);
            }
            bg.sprite = vague[int.Parse(hit.collider.name) - 1];
            past = hit.collider;

            if (Input.GetMouseButtonDown(0))
            {
                DataBox.Data.levelNo = int.Parse(hit.collider.name);
                DataBox.Data.levelE = emp[int.Parse(hit.collider.name) - 1];
                DataBox.Data.levelH = hang[int.Parse(hit.collider.name) - 1];
                DataBox.Data.levelL = lie[int.Parse(hit.collider.name) - 1];
                DataBox.Data.sceneNo = 5;
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            box.enabled = false;
            picn.text = "";
            comp.text = "";
            best.text = "";
        }
    }

    public void ToReturn()
    {
        SceneManager.LoadScene(1);
    }

}
