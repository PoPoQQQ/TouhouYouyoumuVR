using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDanmaku : MonoBehaviour
{
	Vector3 initialPosition;
	Vector3 targetPosition;
	float duration;
	float speed;

	public void Initialize(Vector3 initialPosition, Vector3 targetPosition, float duration, float speed)
	{
		this.initialPosition = initialPosition;
		this.targetPosition = targetPosition;
		this.duration = duration;
		this.speed = speed;
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

    	GameObject player = GameObject.Find("Player");
    	Vector3 direction = (player.transform.position - transform.position).normalized * speed;
    	while(true)
    	{
    		transform.position += direction * Time.deltaTime;
    		yield return 0;
    	}
    }
}
