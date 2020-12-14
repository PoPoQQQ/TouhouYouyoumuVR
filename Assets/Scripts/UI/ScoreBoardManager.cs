using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

public class ScoreBoardManager : MonoBehaviour
{
    public GameObject DigitDisplay;
    public string[] scoreString;
    float distance = 3.0f;
    int scorenum;
    GameObject[] scoreObject;

    List<int> scoreList;

    int cmp(int x, int y)
    {
        return y.CompareTo(x);
    }

    // Start is called before the first frame update
    void Start()
    {
        string dataPath = Application.persistentDataPath + "/ranklist.txt";
        Debug.Log(dataPath);
        //TextAsset scores = (TextAsset)Resources.Load("ranklist");
        //string str = scores.text;
        //scoreString = str.Split('\n');
        if(!File.Exists(dataPath))
        {
            File.Create(dataPath).Dispose();
        }

        scoreList = new List<int>();
        if(GlobalInfo.lastScore != -1)
            scoreList.Add(GlobalInfo.lastScore);
        
        string score;
        StreamReader reader = new StreamReader(dataPath);
        while((score = reader.ReadLine()) != null && scoreList.Count < 10)
        {
            scoreList.Add((int)float.Parse(score));
        }

        reader.Close();
        //foreach(string score in scoreString)
        //    scoreList.Add((int)float.Parse(score));
        
        int scorenum = scoreList.Count;

        scoreObject = new GameObject[scorenum];
        Vector3 pos = new Vector3(0.0f, - (scorenum - 1) * distance / 2.0f, 0.0f);
        for(int i = scorenum - 1; i >= 0; i--)
        {
            scoreObject[i] = Instantiate(DigitDisplay) as GameObject;
            scoreObject[i].GetComponent<DigitDisplay>().displaynumber = scoreList[i];
            scoreObject[i].GetComponent<DigitDisplay>().scale = 3.0f;
            //scoreObject[i].GetComponent<DigitDisplay>().displaynumber = 10;
            scoreObject[i].transform.parent = gameObject.transform;
            scoreObject[i].transform.localPosition = pos;
            pos.y += distance;
        }
        
        if(GlobalInfo.lastScore != -1)
        {
            GlobalInfo.lastScore = -1;
            scoreObject[0].GetComponent<DigitDisplay>().SetColor(new Color(1.0f, 0.5f, 0.5f, 1.0f));
        }

        StreamWriter writer = new StreamWriter(dataPath);
        scoreList.Sort(cmp);
        foreach(int val in scoreList)
        {
            writer.WriteLine(val.ToString());
        }
        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
