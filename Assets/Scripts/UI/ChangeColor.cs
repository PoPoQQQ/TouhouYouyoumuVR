using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public bool change = false;
    // Start is called before the first frame update

    Image image;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(change)
        {
            image.color = Color.red;
        }
        else
        {
            image.color = Color.white;
        } 
    }
}
