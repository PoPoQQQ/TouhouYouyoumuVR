using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDanmakuAdjustion : MonoBehaviour
{
	Camera camera;
    Vector3 lastPosition;
    Vector3 localPosition;

    void Start()
    {
    	camera = GameObject.Find("CameraPosition").GetComponentInChildren<Camera>();
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        Vector3 position = transform.position;
        if(position != lastPosition)
            localPosition = camera.WorldToScreenPoint(position) - camera.WorldToScreenPoint(lastPosition);
        float alpha = Mathf.Atan2(localPosition.y, localPosition.x);
        Vector3 localEulerAngles = transform.localEulerAngles;
        localEulerAngles.z = 90f - alpha * Mathf.Rad2Deg;
        transform.localEulerAngles = localEulerAngles;
        lastPosition = transform.position;
    }
}
