 using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UISelecter : MonoBehaviour
{
    // Start is called before the first frame update
    Transform camera;

    public GameObject background;

    public GameObject start;
    public GameObject option;
    public GameObject result;

    public GameObject title;
    public GameObject loading;
    public GameObject magicCircle;

    void Start()
    {
        GameObject.Find("Player").GetComponentInChildren<AudioManager>().PlayStartBGM();
        camera = GameObject.Find("LeftEye").transform;
        loading.SetActive(false);
        magicCircle.SetActive(false);
    }

    IEnumerator LoadSampleScene()
    {
        background.SetActive(false);
        start.SetActive(false);
        option.SetActive(false);
        result.SetActive(false);
        title.SetActive(false);
        loading.SetActive(true);
        magicCircle.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        start.GetComponent<UISelectBehavior>().isSelecting = false;
        option.GetComponent<UISelectBehavior>().isSelecting = false;
        result.GetComponent<UISelectBehavior>().isSelecting = false;
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
            else if(obj.name == "Start")
            {
                start.GetComponent<UISelectBehavior>().isSelecting = true;
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                {
                    StartCoroutine(LoadSampleScene());
                    //SceneManager.LoadScene("SampleScene");
                }
            }
            else if(obj.name == "Option")
            {
                option.GetComponent<UISelectBehavior>().isSelecting = true;
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                {
                    SceneManager.LoadScene("OptionMenu");
                }
            }
            else if(obj.name == "Result")
            {
                result.GetComponent<UISelectBehavior>().isSelecting = true;
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                {
                    SceneManager.LoadScene("ResultMenu");
                }
            }
        }
    }
}
