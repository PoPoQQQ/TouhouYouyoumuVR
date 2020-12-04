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
    void Start()
    {
        camera = GameObject.Find("LeftEye").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        start.GetComponent<UISelectBehavior>().isSelecting = false;
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
                if(Input.touchCount == 1)
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }
        }
    }
}
