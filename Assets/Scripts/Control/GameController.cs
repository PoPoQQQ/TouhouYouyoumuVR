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
    DigitDisplay digitDisplay;

    bool flag = false;
    public Text[] texts;
    bool dead = false;

    void Start()
    {
        UICanvas.SetActive(false);
        digitDisplay = GameObject.Find("DigitDisplay").GetComponent<DigitDisplay>();
        StartCoroutine(UpdateCoroutine());
    }

    IEnumerator UpdateCoroutine()
    {
        while(true)
        {
            if(flag || Time.timeScale == 0)
            {
                flag = false;
                yield return 0;
                continue;
            }
            if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
            {
                OnPause();
            }
            yield return 0;
        }
    }

    public void OnPause()
    {
        foreach(Text text in texts)
            text.text = "一時停止";
        UICanvas.SetActive(true);
        Time.timeScale = 0;
        hideObjs = new List<GameObject>();
        hideObjs.Add(GameObject.Find("判定区1"));
        GameObject.Find("判定区1").SetActive(false);
        hideObjs.Add(GameObject.Find("判定区2"));
        GameObject.Find("判定区2").SetActive(false);
        hideObjs.Add(GameObject.Find("判定区3"));
        GameObject.Find("判定区3").SetActive(false);
        //hideObjs.Add(GameObject.Find("ScoreCanvas"));
        //GameObject.Find("ScoreCanvas").SetActive(false);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PauseBGM();
        dead = false;
    }

    public void OnHPEmpty()
    {
        foreach(Text text in texts)
            text.text = "満身創痍";
        UICanvas.SetActive(true);
        Time.timeScale = 0;
        hideObjs = new List<GameObject>();

        hideObjs.Add(GameObject.Find("判定区1"));
        GameObject.Find("判定区1").SetActive(false);
        hideObjs.Add(GameObject.Find("判定区2"));
        GameObject.Find("判定区2").SetActive(false);
        hideObjs.Add(GameObject.Find("判定区3"));
        GameObject.Find("判定区3").SetActive(false);
        //hideObjs.Add(GameObject.Find("ScoreCanvas"));
        //GameObject.Find("ScoreCanvas").SetActive(false);
        dead = true;
        GlobalInfo.deathCount += 1;

        GameObject.Find("AudioManager").GetComponent<AudioManager>().PauseBGM();
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
        if(dead)
        {
            digitDisplay.displaynumber = GlobalInfo.deathCount;
            GameObject.Find("HitArea").GetComponent<PlayerCollider>().ResetHp();
        }
        GameObject.Find("AudioManager").GetComponent<AudioManager>().ResumeBGM();
        flag = true;
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1;
        int score = digitDisplay.displaynumber;
        GlobalInfo.ReportScore(score);
        GlobalInfo.deathCount = 0;
        SceneManager.LoadScene("ResultMenu");
        flag = true;
    }
}
