using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Material material;
    public float speed;

    void Update()
    {
    	Vector4 vector = material.GetVector("_MainTex_ST");
    	vector.w += speed * Time.deltaTime;
    	vector.w -= (int)vector.w;
    	material.SetVector("_MainTex_ST", vector);
    }

    void OnDisable()
    {
    	Vector4 vector = material.GetVector("_MainTex_ST");
    	vector.w = 0f;
    	material.SetVector("_MainTex_ST", vector);
    }
}
