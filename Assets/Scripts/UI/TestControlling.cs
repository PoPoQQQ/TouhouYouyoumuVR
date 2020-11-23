using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControlling : MonoBehaviour
{
    float movingSpeed;
    float boundary;

    void Start()
    {
        movingSpeed = GetComponent<PlayerMoving>().movingSpeed;
        boundary = GetComponent<PlayerMoving>().boundary;
    }

    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        if(direction == Vector3.zero)
        	return;
        transform.position += direction.normalized * movingSpeed * Time.deltaTime;
        if(transform.position.magnitude > boundary)
        	transform.position = transform.position.normalized * boundary;
    }
}
