using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background2 : MonoBehaviour
{
    public float speed;

    void Update()
    {
    	transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}
