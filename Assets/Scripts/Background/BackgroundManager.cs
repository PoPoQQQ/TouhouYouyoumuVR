using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public int currentBackground = 0;
    public GameObject[] backgrounds;

    void Start()
    {
    	for(int i = 0; i < backgrounds.Length; i++)
    		backgrounds[i].SetActive(i == currentBackground);
    }

    public void SetBackground(int index)
    {
    	if(index >= backgrounds.Length)
    		return;
    	backgrounds[currentBackground].SetActive(false);
    	currentBackground = index;
    	backgrounds[currentBackground].SetActive(true);
    }
}
