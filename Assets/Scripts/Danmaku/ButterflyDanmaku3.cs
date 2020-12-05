using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyDanmaku3 : MonoBehaviour
{
	Vector3 initialPosition;
	float initialAngle;
	bool reverse;
	Vector2 finalDirection;
    float finalSpeed;

	public void Initialize(Vector3 initialPosition, float initialAngle, bool reverse, float finalSpeed)
	{
		this.initialPosition = initialPosition;
		this.initialAngle = initialAngle;
		this.reverse = reverse;
        this.finalSpeed = finalSpeed;
	}

	void Start()
	{
		StartCoroutine(ButterflyDanmakuCoroutine());
	}

    IEnumerator ButterflyDanmakuCoroutine()
    {
    	StartCoroutine(ZMovingCoroutine());
    	yield return StartCoroutine(ButterflyDanmakuCoroutine2());
        gameObject.AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, GameObject.Find("Player").transform.position - transform.position, finalSpeed);
        Destroy(this);
    }

    IEnumerator ZMovingCoroutine()
    {
    	float zSpeed = 0f;
    	while(true)
    	{
    		transform.position += Vector3.back * zSpeed * Time.deltaTime;
    		zSpeed += 15f * Time.deltaTime;
    		yield return 0;
    	}
    }

    IEnumerator ButterflyDanmakuCoroutine2()
    {
    	float duration = 2f;
    	float deltaRadius = 20f;
    	float deltaAngle = 120f * (reverse ? -1f : 1f);
    	Vector2 initialPosition = this.initialPosition;
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
}
