using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirits : MonoBehaviour
{
    Vector3 initialPosition;
    Vector3 targetPosition;
	float duration;

	public void Initialize(Vector3 initialPosition, Vector3 targetPosition, float duration)
	{
		this.initialPosition = initialPosition;
		this.targetPosition = targetPosition;
		this.duration = duration;
	}

	void Start()
	{
		StartCoroutine(SpiritsCoroutine());
	}

    IEnumerator SpiritsCoroutine()
    {
    	Transform[] spirits = new Transform[8];
    	for(int i = 0; i < 8; i++)
    		spirits[i] = transform.GetChild(i);
    	for(float t = 0; t < duration; t += Time.deltaTime)
    	{
    		float rate1 = t / duration;
    		float rate2 = -4 * rate1 * rate1 + 4 * rate1;
    		float deltaAngle = rate1 * 90f;
    		float radius = rate2 * 30f;
    		transform.position = initialPosition * (1 - rate1) + targetPosition * rate1;
    		for(int i = 0; i < 8; i++)
    		{
    			float angle = i * 45f + deltaAngle;
    			spirits[i].localPosition = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * radius;
    		}
    		yield return 0;
    	}
    	yield return new WaitForSeconds(5f);
    	Destroy(gameObject);
    }
}
