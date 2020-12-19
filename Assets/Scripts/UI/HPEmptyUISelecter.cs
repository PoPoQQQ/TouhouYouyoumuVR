using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class HPEmptyUISelecter : MonoBehaviour
{
    // Start is called before the first frame update
    Transform camera;

    public GameObject BackToGame;

    public GameObject ReturnToTitle;

    void Start()
    {
        camera = GameObject.Find("CameraPosition").GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        ReturnToTitle.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
        BackToGame.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
        if(Physics.Raycast(camera.position, camera.forward, out hit, Mathf.Infinity, 1))
        {
            GameObject obj = hit.collider.gameObject;
            Debug.Log("hit object : " +  obj.name);
            if(obj.name == "ReturnToTitle")
            {
                ReturnToTitle.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                {
                    GameObject.Find("GameController").GetComponent<GameController>().ReturnToTitle();
                }
            }
            else if(obj.name == "BackToGame")
            {
                BackToGame.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                if((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                {
                    GameObject.Find("GameController").GetComponent<GameController>().BackToGame();
                }
            }
        }
    }
}
