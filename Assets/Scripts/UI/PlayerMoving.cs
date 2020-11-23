using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
	public float threshold;
	public float movingSpeed;
	public float boundary;

	Transform camera;

	void Start()
	{
		camera = GetComponentInChildren<Camera>().transform;
	}

    void Update()
    {
        Vector2 direction = camera.transform.forward.normalized;
        if(direction.magnitude < threshold)
        	return;
        transform.position += (Vector3)(direction.normalized * movingSpeed * Time.deltaTime);
        if(transform.position.magnitude > boundary)
        	transform.position = transform.position.normalized * boundary;
    }
}
