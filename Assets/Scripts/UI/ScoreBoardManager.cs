using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardManager : MonoBehaviour
{
    public GameObject DigitDisplay;
    public string[] scoreList;
    float distance = 3.0f;
    int scorenum;
    GameObject[] scoreObject;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset scores = (TextAsset)Resources.Load("ranklist");
        string str = scores.text;
        scoreList = str.Split('\n');
        scorenum = scoreList.Length;
        scoreObject = new GameObject[scorenum];
        Vector3 pos = new Vector3(0.0f, - (scorenum - 1) * distance / 2.0f, 0.0f);
        for(int i = 0; i < scorenum; i++)
        {
            scoreObject[i] = Instantiate(DigitDisplay) as GameObject;
            scoreObject[i].GetComponent<DigitDisplay>().displaynumber = (int)float.Parse(scoreList[i]);
            scoreObject[i].GetComponent<DigitDisplay>().scale = 3.0f;
            //scoreObject[i].GetComponent<DigitDisplay>().displaynumber = 10;
            scoreObject[i].transform.parent = gameObject.transform;
            scoreObject[i].transform.localPosition = pos;
            pos.y += distance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
