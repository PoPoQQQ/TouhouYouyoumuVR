using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class OptionUISelecter : MonoBehaviour
{
    // Start is called before the first frame update
    Transform camera;

    public GameObject background;

    public GameObject title;
    
    public GameObject easy_level;
    public GameObject normal_level;
    public GameObject hard_level;
    public GameObject lunatic_level;
    public GameObject extra_level;
    public GameObject phantasm_level;

    void Start()
    {
        GameObject.Find("Player").GetComponentInChildren<AudioManager>().PlayStartBGM();
        camera = GameObject.Find("LeftEye").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        GlobalInfo.Difficulty diff = GlobalInfo.currDifficulty;
        title.GetComponent<UISelectBehavior>().isSelecting = false;
        easy_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Easy) ? true : false;
        normal_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Normal) ? true : false;
        hard_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Hard) ? true : false;
        lunatic_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Lunatic) ? true : false;
        extra_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Extra) ? true : false;
        phantasm_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Phantasm) ? true : false;
        if(Physics.Raycast(camera.position, camera.forward, out hit))
        {
            GameObject obj = hit.collider.gameObject;
            Debug.Log("hit object : " +  obj.name);
            if(obj.name == "Background")
            {
                /*background.GetComponent<ChangeColor>().change = true;
                if(Input.touchCount == 1)
                {
                    background.GetComponent<ChangeColor>().change = false;
                    //SceneManager.LoadScene("SampleScene");
                }*/
            }
            else if(obj.name == "Title")
            {
                title.GetComponent<UISelectBehavior>().isSelecting = true;
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                {
                    SceneManager.LoadScene("StartMenu");
                }
            }
            else if(obj.name == "easy")
            {
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                    GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Easy;
            }
            else if(obj.name == "normal")
            {
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                    GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Normal;
            }
            else if(obj.name == "hard")
            {
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                    GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Hard;
            }
            else if(obj.name == "lunatic")
            {
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                    GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Lunatic;
            }
            else if(obj.name == "extra")
            {
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                    GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Extra;
            }
            else if(obj.name == "phantasm")
            {
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                    GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Phantasm;
            }
        }
    }
}
