using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyDanmaku2 : MonoBehaviour
{
	Vector3 initialPosition;
    float rSpeed;
    float zSpeed;
	float initialAngle;
	float omega;
    float duration;
	float finalSpeed;
    Vector3 finalDirection;

	public void Initialize(Vector3 initialPosition, float rSpeed, float zSpeed, float initialAngle, float omega, float duration, float finalSpeed)
	{
		this.initialPosition = initialPosition;
        this.rSpeed = rSpeed;
        this.zSpeed = zSpeed;
		this.initialAngle = initialAngle;
		this.omega = omega;
        this.duration = duration;
		this.finalSpeed = finalSpeed;
	}

	void Start()
	{
		StartCoroutine(ButterflyDanmakuCoroutine());
	}

    IEnumerator ButterflyDanmakuCoroutine()
    {
    	yield return StartCoroutine(ButterflyDanmakuCoroutine2());
        yield return StartCoroutine(ButterflyDanmakuCoroutine3());
    }

    IEnumerator ButterflyDanmakuCoroutine2()
    {
        Vector3 lastPosition = transform.position;
        float t;
    	for(t = 0; t < duration; t += Time.deltaTime)
    	{
    		float r = rSpeed * t;
            float z = zSpeed * t;
    		float angle = initialAngle + omega * t;
    		lastPosition = transform.position = initialPosition + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * r + Vector3.back * z;
    		yield return 0;
    	}
        {
            float r = rSpeed * t;
            float z = zSpeed * t;
            float angle = initialAngle + omega * t;
            Vector3 position = initialPosition + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * r + Vector3.back * z;
            finalDirection = (position - lastPosition).normalized;
        }
    }

    IEnumerator ButterflyDanmakuCoroutine3()
    {
    	while(true)
    	{
    		transform.position += finalDirection * finalSpeed * Time.deltaTime;
    		yield return 0;
    	}
    }
}
