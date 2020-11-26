using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningDanmaku : MonoBehaviour
{
	Vector3 initialPosition;
	Vector3 delta;
    float lifeTime;
    const float turningDuration = 0.03f;
    float turningTime;
    Vector3 turningCenter;
    float speed;
    bool turnStarted;
    public void InitializeWithRay(Vector3 initialPosition, Vector3 direction, float speed, float turningTime, Vector3 turningCenter)
	{
		this.initialPosition = initialPosition;
		this.delta = direction.normalized;
        this.speed = speed;
        lifeTime = 0;
        this.turningTime = turningTime;
        this.turningCenter = turningCenter;
        turnStarted = false;
    }

	void Start()
	{
		transform.position = initialPosition;
	}

    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= turningTime && lifeTime <= turningTime + turningDuration)
        {
            if (turnStarted == false)
            {
                turnStarted = true;
                turningCenter.z = 0;
                turningCenter.Normalize();
                Vector3 direction = new Vector3(turningCenter.x, turningCenter.y, 0f) * 50f - transform.position;
                delta = direction.normalized;
            }
            //delta = Vector3.Cross(turningCenter - transform.position, Vector3.Cross(delta,turningCenter - transform.position)).normalized;
        }
        transform.position += delta  * speed * Time.deltaTime;
    }
}
