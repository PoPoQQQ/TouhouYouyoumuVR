using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool pause = false;

    public GameObject UICanvas;

    List<GameObject> hideObjs;

    int deathCount = 0;
    void Start()
    {
        UICanvas.SetActive(false);
    }

    public void OnHPEmpty()
    {
        UICanvas.SetActive(true);
        Time.timeScale = 0;
        hideObjs = new List<GameObject>();

        hideObjs.Add(GameObject.Find("判定区"));
        GameObject.Find("判定区").SetActive(false);
        hideObjs.Add(GameObject.Find("ScoreCanvas"));
        GameObject.Find("ScoreCanvas").SetActive(false);
        GlobalInfo.deathCount += 1;

        GameObject[] danmakus = GameObject.FindGameObjectsWithTag("Danmaku");
        foreach(GameObject obj in danmakus)
        {
            //obj.GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
            //hideObjs.Add(obj);
            obj.SetActive(false);
        }
        GameObject.Find("AudioManager").GetComponent<AudioSource>().Pause();
    }

    public void BackToGame()
    {
        UICanvas.SetActive(false);
        Time.timeScale = 1;

        foreach(GameObject obj in hideObjs)
        {
            if(obj != null)
                obj.SetActive(true);
        }
        GameObject.Find("DigitDisplay").GetComponent<DigitDisplay>().displaynumber = GlobalInfo.deathCount;
        GameObject.Find("HitArea").GetComponent<PlayerCollider>().ResetHp();
        GameObject.Find("AudioManager").GetComponent<AudioSource>().Play();
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1;
        foreach(GameObject obj in hideObjs)
        {
            if(obj != null)
                obj.SetActive(true);
        }
        int score = GameObject.Find("DigitDisplay").GetComponent<DigitDisplay>().displaynumber;
        GlobalInfo.ReportScore(score);
        SceneManager.LoadScene("ResultMenu");
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(!pause)
            {
                pause = true;
                Time.timeScale = 0;
            }
            else
            {
                pause = false;
                Time.timeScale = 1;
            }
        }*/
        /*if(pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;*/
    }
}
