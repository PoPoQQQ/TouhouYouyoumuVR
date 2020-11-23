using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDanmaku : MonoBehaviour
{
	Vector3 initialPosition;
	Vector3 targetPosition;
	float duration;
	float zSpeed;
    float omega;

	public void Initialize(Vector3 initialPosition, Vector3 targetPosition, float duration, float zSpeed, float omega)
	{
		this.initialPosition = initialPosition;
		this.targetPosition = targetPosition;
		this.duration = duration;
		this.zSpeed = zSpeed;
        this.omega = omega;
	}

	void Start()
	{
		StartCoroutine(DanmakuCoroutine());
	}

    IEnumerator DanmakuCoroutine()
    {
    	for(float t = 0; t < duration; t += Time.deltaTime)
    	{
    		float rate = Mathf.Pow(t / duration, 0.5f);
    		transform.position = initialPosition * (1 - rate) + targetPosition * rate;
    		yield return 0;
    	}
    	transform.position = targetPosition;

    	while(true)
    	{
    		Vector3 position = transform.position;
            position = Quaternion.AngleAxis(omega * Time.deltaTime, Vector3.forward) * position;
            position.z -= zSpeed * Time.deltaTime;
            transform.position = position;
    		yield return 0;
    	}
    }
}
