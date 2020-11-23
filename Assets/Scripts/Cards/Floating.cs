using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
	public float amplitude;
	public float frequency;
	public float phase;

	float Y(float t)
	{
		float ret = Mathf.Sin(t * frequency + phase) * amplitude;
		return ret * ret;
	}

	float DeltaY()
	{
		return Y(Time.time) - Y(Time.time - Time.deltaTime);
	}

    void Update()
    {
    	transform.localPosition += new Vector3(0, DeltaY(), 0);
    }
}
