using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralRayDanmaku : MonoBehaviour
{
	Vector3 initialPosition;
    float initialPhi;
    float finalPhi;
    float radius;
    float spireZ;
    Vector3 finalDestination;
    float spiralTime;
    bool towardsPlayer;
    float lifeTime;
    Vector3 rayOrigin;
    float speed;
    public void InitializeWithDestination(
        Vector3 initialPosition, float initialPhi, float finalPhi, float radius, float spireZ, float spiralTime, float speed, 
        Vector3 finalDestination)
    {
        this.initialPosition = initialPosition;
        this.initialPhi = initialPhi;
        this.finalPhi = finalPhi;
        this.radius = radius;
        this.spireZ = spireZ;
        this.spiralTime = spiralTime;
        this.finalDestination = finalDestination;
        this.speed = speed;
        rayOrigin = initialPosition + new Vector3(radius * Mathf.Cos(finalPhi), radius * Mathf.Sin(finalPhi), 0f);
        rayOrigin.z = spireZ;
        towardsPlayer = false;
    }
    public void InitializeTowardsPlayer(
        Vector3 initialPosition, float initialPhi, float finalPhi, float radius, float spireZ, float spiralTime, float speed)
	{
		this.initialPosition = initialPosition;
        this.initialPhi = initialPhi;
        this.finalPhi = finalPhi;
        this.radius = radius;
        this.spireZ = spireZ;
        this.spiralTime = spiralTime;
        this.speed = speed;
        rayOrigin = initialPosition + new Vector3(radius * Mathf.Cos(finalPhi), radius * Mathf.Sin(finalPhi), 0f);
        rayOrigin.z = spireZ;
        towardsPlayer = true;
    }

	void Start()
	{
		transform.position = initialPosition;
        lifeTime = 0;
	}

    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime < spiralTime)
        {
            float a = lifeTime / spiralTime;
            float phi = a * (finalPhi - initialPhi) + initialPhi;
            float r = radius * a;
            transform.position = initialPosition + new Vector3(Mathf.Cos(phi) * r, Mathf.Sin(phi) * r, (spireZ - initialPosition.z) * a);
        }
        else
        {
            if (towardsPlayer)
            {
                finalDestination = GameObject.Find("Player").transform.position;
                finalDestination.z += 20f;
                towardsPlayer = false;
            }
            Vector3 dir = (finalDestination - rayOrigin).normalized;
            transform.position = rayOrigin + (lifeTime - spiralTime) * speed * dir;
        }
    }
}
