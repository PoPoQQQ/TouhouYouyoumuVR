using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCControlling : MonoBehaviour
{
    void Update()
    {
    	Vector2 position = Input.mousePosition;
    	position.x = Mathf.Clamp(position.x / Screen.width, 0f, 1f);
    	position.y = Mathf.Clamp(position.y / Screen.height, 0f, 1f);
    	position = position * 2f - new Vector2(1f, 1f);
    	Vector3 x = new Vector3(position.x, 0, Mathf.Sqrt(1 - position.x * position.x));
    	Vector3 y = new Vector3(0, position.y, Mathf.Sqrt(1 - position.y * position.y));
    	Vector3 v = x + y - Vector3.forward;
    	transform.LookAt(transform.position + v);
    }
}
