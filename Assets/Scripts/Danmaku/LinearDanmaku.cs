using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearDanmaku : MonoBehaviour
{
	Vector3 initialPosition;
	Vector3 delta;

	public void InitializeWithRay(Vector3 initialPosition, Vector3 direction, float speed)
	{
		this.initialPosition = initialPosition;
		this.delta = direction.normalized * speed;
	}

	public void InitializeWithSegment(Vector3 initialPosition, Vector3 target, float duration)
	{
		this.initialPosition = initialPosition;
		this.delta = (target - initialPosition) / duration;
	}

	void Start()
	{
		transform.position = initialPosition;
	}

    void Update()
    {
    	transform.position += delta * Time.deltaTime;
    }
}
