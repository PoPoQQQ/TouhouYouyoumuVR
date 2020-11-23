using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAdjustion : MonoBehaviour
{
    Transform camera;

	void Start()
	{
		camera = GameObject.Find("CameraPosition").transform;
	}

    void Update()
    {
        Vector3 localPosition = transform.parent.InverseTransformPoint(camera.position);
        float alpha = Mathf.Atan2(localPosition.y, localPosition.z);
        transform.localEulerAngles = new Vector3(-alpha * Mathf.Rad2Deg, 0f, 0f);
    }
}
