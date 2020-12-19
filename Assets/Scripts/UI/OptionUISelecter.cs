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

    GlobalInfo.Difficulty staring;

    void Start()
    {
        camera = GameObject.Find("Player").GetComponentInChildren<Camera>().transform;
        StartCoroutine(UpdateCoroutine());
    }

    // Update is called once per frame
    IEnumerator UpdateCoroutine()
    {
        while(true)
        {
            RaycastHit hit;
            GlobalInfo.Difficulty diff = GlobalInfo.currDifficulty;
            title.GetComponent<UISelectBehavior>().isSelecting = false;
            easy_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Easy || staring == GlobalInfo.Difficulty.Easy);
            normal_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Normal || staring == GlobalInfo.Difficulty.Normal);
            hard_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Hard || staring == GlobalInfo.Difficulty.Hard);
            lunatic_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Lunatic || staring == GlobalInfo.Difficulty.Lunatic);
            extra_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Extra || staring == GlobalInfo.Difficulty.Extra);
            phantasm_level.GetComponent<UISelectBehavior>().isSelecting = (diff == GlobalInfo.Difficulty.Phantasm || staring == GlobalInfo.Difficulty.Phantasm);
            if(Physics.Raycast(camera.position, camera.forward, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                Debug.Log("hit object : " +  obj.name);
                if(obj.name == "Title")
                {
                    staring = GlobalInfo.Difficulty.Undefined;
                    title.GetComponent<UISelectBehavior>().isSelecting = true;
                    if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                    {
                        yield return 0;
                        SceneManager.LoadScene("StartMenu");
                    }
                }
                else if(obj.name == "easy")
                {
                    staring = GlobalInfo.Difficulty.Easy;
                    if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                        GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Easy;
                }
                else if(obj.name == "normal")
                {
                    staring = GlobalInfo.Difficulty.Normal;
                    if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                        GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Normal;
                }
                else if(obj.name == "hard")
                {
                    staring = GlobalInfo.Difficulty.Hard;
                    if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                        GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Hard;
                }
                else if(obj.name == "lunatic")
                {
                    staring = GlobalInfo.Difficulty.Lunatic;
                    if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                        GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Lunatic;
                }
                else if(obj.name == "extra")
                {
                    staring = GlobalInfo.Difficulty.Extra;
                    if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                        GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Extra;
                }
                else if(obj.name == "phantasm")
                {
                    staring = GlobalInfo.Difficulty.Phantasm;
                    if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                        GlobalInfo.currDifficulty = GlobalInfo.Difficulty.Phantasm;
                }
                else
                    staring = GlobalInfo.Difficulty.Undefined;
            }
            else
                staring = GlobalInfo.Difficulty.Undefined;
            yield return 0;
        }
    }
}
