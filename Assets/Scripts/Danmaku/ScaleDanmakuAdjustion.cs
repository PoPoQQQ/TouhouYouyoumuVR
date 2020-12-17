using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDanmakuAdjustion : MonoBehaviour
{
	Camera camera;
    Vector3 lastPosition;

    void Start()
    {
    	camera = GameObject.Find("CameraPosition").GetComponentInChildren<Camera>();
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        Vector3 position = transform.position;
        //Vector3 deltaPosition = position - lastPosition;
        Vector3 localPosition = camera.WorldToScreenPoint(position) - camera.WorldToScreenPoint(lastPosition);
        //if((Vector2)localPosition == Vector2.zero)
        //	return;
        //Debug.Log(localPosition);
        float alpha = Mathf.Atan2(localPosition.y, localPosition.x);
        Vector3 localEulerAngles = transform.localEulerAngles;
        localEulerAngles.z = 90f - alpha * Mathf.Rad2Deg;
        transform.localEulerAngles = localEulerAngles;
        lastPosition = transform.position;
    }
}
