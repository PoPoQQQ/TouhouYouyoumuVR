using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDanmaku2 : MonoBehaviour
{
    Vector3 initialPosition;
    float initialAngle;
    float omega;
	float rSpeed;
	float zSpeed;

	public void Initialize(Vector3 initialPosition, float initialAngle, float omega, float rSpeed, float zSpeed)
	{
		this.initialPosition = initialPosition;
		this.initialAngle = initialAngle;
		this.omega = omega;
		this.rSpeed = rSpeed;
		this.zSpeed = zSpeed;
	}

	void Start()
	{
		StartCoroutine(DanmakuCoroutine());
	}

    IEnumerator DanmakuCoroutine()
    {
    	for(float t = 0;; t += Time.deltaTime)
    	{
    		float angle = initialAngle + t * omega;
    		transform.position = initialPosition + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * t * rSpeed + Vector3.back * t *zSpeed;
    		yield return 0;
    	}
    }
}
