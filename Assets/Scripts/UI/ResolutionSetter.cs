using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSetter : MonoBehaviour
{
    void Start()
    {
        SetResolution();
    }

    void SetResolution()
    {
        float heightScale = 9.0f;
        float widthScale = 16.0f;
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;
        int width = Screen.width;
        int height = Screen.height;
        if (((widthScale * height) / heightScale) > screenWidth)
        {
            int h = (int)((heightScale * screenWidth) / widthScale);
            int w = (int)((widthScale * h) / heightScale);
            Screen.SetResolution(w, h, true);
        }
        else
        {
            int w = (int)((widthScale * screenHeight) / heightScale);
            int h = (int)((heightScale * w) / widthScale);
            Screen.SetResolution(w, h, true);
        }
    }

}
