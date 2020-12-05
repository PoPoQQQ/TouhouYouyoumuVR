using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipDanmaku : MonoBehaviour
{
	Vector3 initialPosition;
    Vector3 turningPosition;
    public Vector3 finalDestination;
    bool flag;
    float speed;
    Vector3 finalOffset;
    WhipDanmaku tip;
    public void InitializeWithPoints(Vector3 initialPosition, Vector3 turningPosition, Vector3 finalOffset, WhipDanmaku tip, float speed)
	{
		this.initialPosition = initialPosition;
        this.turningPosition = turningPosition;
        this.finalOffset = finalOffset;
        if (tip == null)
        {
            tip = this;
        }
        this.tip = tip;
        this.speed = speed;
        flag = false;
    }

	void Start()
	{
		transform.position = initialPosition;
	}

    void Update()
    {
        float t = (initialPosition.z - transform.position.z) / ((initialPosition.z - turningPosition.z) / 2 )* Mathf.PI / 2;
        t -= Mathf.PI / 2;
        t = Mathf.Max(0f, t);
        t = Mathf.Min(Mathf.PI, t);
        float a = 1f - 0.5f * Mathf.Sin(t);
        a = a * a;
        if (transform.position.z > turningPosition.z)
        {
            transform.position += (turningPosition - initialPosition).normalized * speed * a * Time.deltaTime;
        }
        else
        {
            if (tip == this)
            {
                if (!flag)
                {
                    flag = true;
                    finalDestination = GameObject.Find("Player").transform.position;
                }
            }
            transform.position += (tip.finalDestination + finalOffset - turningPosition).normalized * speed * a * Time.deltaTime;
        }
    }
}
