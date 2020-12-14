using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DigitDisplay : MonoBehaviour
{
    public Sprite[] digits;
    public int length = 10;
    public int displaynumber = 0;
    public GameObject digitPrefab;

    public float scale = 1.0f;

    float distance = 0.6f;

    GameObject[] digitObject;
    int[] currDigit;
    int lastnumber;
    float lastscale;

    Color lastColor = Color.white, color = Color.white;

    void updateDigit()
    {
        for(int i = length - 1, val = displaynumber; i >= 0; i--)
        {
            currDigit[i] = val % 10;
            val /= 10;
        }
        for(int i = 0; i < length; i++)
        {
            Image image = digitObject[i].GetComponent<Image>();
            image.sprite = digits[currDigit[i]];
        }
        lastnumber = displaynumber;
    }

    void updateScale()
    {
        Vector3 pos = new Vector3( - (length - 1) * distance * scale / 2.0f, 0.0f, 0.0f);
        for(int i = 0; i < length; i++)
        {
            digitObject[i].transform.localPosition = pos;
            digitObject[i].transform.localScale = new Vector3(scale, scale, scale);
            pos.x += distance * scale;
        }
        lastscale = scale;
    }

    void updateColor()
    {
        foreach(GameObject digit in digitObject)
        {
            digit.GetComponent<Image>().color = color;
        }
        lastColor = color;
    }

    // Start is called before the first frame update
    void Start()
    {
        currDigit = new int[length];
        digitObject = new GameObject[length];
        //float width = digitPrefab.GetComponent<RectTransform>().rect.width;
        Vector3 pos = new Vector3( - (length - 1) * distance / 2.0f, 0.0f, 0.0f);
        for(int i = 0; i < length; i++)
        {
            digitObject[i] = Instantiate(digitPrefab) as GameObject;
            digitObject[i].transform.parent = gameObject.transform;
            digitObject[i].transform.localPosition = pos;
            Image image = digitObject[i].GetComponent<Image>();
            image.sprite = digits[currDigit[i]];
            image.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
            pos.x += distance;
        }
        updateDigit();
        updateScale();
    }

    public void SetColor(Color t_color)
    {
        color = t_color;
    }

    // Update is called once per frame
    void Update()
    {
        if(displaynumber != lastnumber)
            updateDigit();
        if(scale != lastscale)
            updateScale();
        if(lastColor != color)
            updateColor();
    }
}
