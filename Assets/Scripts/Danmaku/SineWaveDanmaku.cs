using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveDanmaku : MonoBehaviour
{
	Vector3 randomPosition;
	Vector3 initialPosition;
	Vector3 lastPosition;
	float speed;
	float range = 0;

	public void Initialize(Vector3 initialPosition, float speed)
	{
		this.initialPosition = initialPosition;
		this.speed = speed;
		this.randomPosition = Random.insideUnitSphere;
	}

	void Start()
	{
		transform.localPosition = initialPosition;
	}

    void Update()
    {
    	if(Time.deltaTime == 0)
    		return;
    	float deltaZ = speed * Time.deltaTime;
    	range += deltaZ;
    	if(range * 0.2f > Mathf.PI * 3.5f)
    	{
    		Vector3 direction = transform.position - lastPosition;
    		gameObject.AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, direction, speed);
    		transform.parent = null;
    		Destroy(this);
    		return;
    	}
    	lastPosition = transform.position;
    	transform.localPosition = new Vector3(0f, 5f * Mathf.Sin(range * 0.2f), range) + randomPosition * range * range * range * 5.45e-5f;
    }
}
