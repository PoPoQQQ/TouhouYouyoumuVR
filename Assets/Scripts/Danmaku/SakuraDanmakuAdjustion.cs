using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakuraDanmakuAdjustion : MonoBehaviour
{
	Vector2 originPosition;

    void Start()
    {
        originPosition = transform.position;
    }

    void Update()
    {
        Vector2 position = transform.position;
        Vector2 deltaPosition = position - originPosition;
        float alpha = Mathf.Atan2(deltaPosition.y, deltaPosition.x);
        Vector3 localEulerAngles = transform.localEulerAngles;
        localEulerAngles.z = 90f - alpha * Mathf.Rad2Deg;
        transform.localEulerAngles = localEulerAngles;
    }
}
