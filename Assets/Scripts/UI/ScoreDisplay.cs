using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
	Transform camera;
    Vector3 relative_position;

	void Start()
	{
		camera = GameObject.Find("CameraPosition").transform;
        relative_position = transform.position - camera.position;
	}

    void Update()
    {
        //transform.LookAt(camera);
        //transform.position = camera.position + relative_position;
        //transform.Rotate(0, 180, 0);
    }
}
