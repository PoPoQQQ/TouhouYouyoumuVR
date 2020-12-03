using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyDanmaku : MonoBehaviour
{
	Vector3 initialPosition;
	Vector3 targetPosition;
	float initialAngle;
	bool reverse;
	Vector2 finalDirection;
	float xySpeed;

	public void Initialize(Vector3 initialPosition, Vector3 targetPosition, float initialAngle, bool reverse, float xySpeed)
	{
		this.initialPosition = initialPosition;
		this.targetPosition = targetPosition;
		this.initialAngle = initialAngle;
		this.reverse = reverse;
		this.xySpeed = xySpeed;
	}

	void Start()
	{
		StartCoroutine(ButterflyDanmakuCoroutine());
	}

    IEnumerator ButterflyDanmakuCoroutine()
    {
    	yield return StartCoroutine(ButterflyDanmakuCoroutine1());
    	StartCoroutine(ZMovingCoroutine());
    	yield return StartCoroutine(ButterflyDanmakuCoroutine2());
    	yield return StartCoroutine(ButterflyDanmakuCoroutine3());
    }

    IEnumerator ZMovingCoroutine()
    {
    	float zSpeed = 0f;
    	while(true)
    	{
    		transform.position += Vector3.back * zSpeed * Time.deltaTime;
    		zSpeed += 5f * Time.deltaTime;
    		yield return 0;
    	}
    }

    IEnumerator ButterflyDanmakuCoroutine1()
    {
    	float duration = 1f;
    	for(float t = 0; t < duration; t += Time.deltaTime)
    	{
    		float rate = 1f - Mathf.Pow(1f - t / duration, 2f);
    		transform.position = initialPosition * (1 - rate) + targetPosition * rate;
    		yield return 0;
    	}
    	transform.position = targetPosition;
    }

    IEnumerator ButterflyDanmakuCoroutine2()
    {
    	float duration = 2f;
    	float deltaRadius = 20f;
    	float deltaAngle = 120f * (reverse ? -1f : 1f);
    	Vector2 initialPosition = transform.position;
    	Vector2 lastPosition = Vector2.zero;
    	for(float t = 0; t < duration; t += Time.deltaTime)
    	{
    		float rate = t / duration;
    		float radius = deltaRadius * rate;
    		float angle = initialAngle + deltaAngle * rate;
    		lastPosition = initialPosition + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
    		transform.position = new Vector3(lastPosition.x, lastPosition.y, transform.position.z);
    		yield return 0;
    	}
    	{
    		Vector2 position = initialPosition + new Vector2(Mathf.Cos((initialAngle + deltaAngle) * Mathf.Deg2Rad), Mathf.Sin((initialAngle + deltaAngle) * Mathf.Deg2Rad)) * deltaRadius;
    		transform.position = new Vector3(position.x, position.y, transform.position.z);
    		finalDirection = (position - lastPosition).normalized;
    	}
    }

    IEnumerator ButterflyDanmakuCoroutine3()
    {
    	while(true)
    	{
    		transform.position += (Vector3)(finalDirection * xySpeed * Time.deltaTime);
    		yield return 0;
    	}
    }
}
