using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving2 : MonoBehaviour
{
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
        transform.position += (Vector3)(direction * movingSpeed * Time.deltaTime * Mathf.Min(1f / direction.magnitude, 2.5f));
        if(transform.position.magnitude > boundary)
        	transform.position = transform.position.normalized * boundary;
    }
}
