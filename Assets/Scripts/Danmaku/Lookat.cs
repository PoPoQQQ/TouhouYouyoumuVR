using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
	Transform camera;

	void Start()
	{
		camera = GameObject.Find("CameraPosition").transform;
	}

    void Update()
    {
        transform.LookAt(camera);
    }
}
