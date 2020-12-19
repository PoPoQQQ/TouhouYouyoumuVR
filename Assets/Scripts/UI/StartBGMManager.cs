using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBGMManager : MonoBehaviour
{
	static GameObject instance = null;

    void Start()
    {
        if(instance == null || !instance)
        {
        	DontDestroyOnLoad(gameObject);
        	instance = gameObject;
        }
        else
        	Destroy(gameObject);
    }

    void Update()
    {
    	if(SceneManager.GetActiveScene().name == "SampleScene")
    	{
    		instance = null;
    		Destroy(gameObject);
    	}
    }
}
