using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCard1 : MonoBehaviour
{
    GameObject skySakuraDanmaku;
    GameObject yellowSakuraDanmaku;
    GameObject skyTamaDanmaku;
    GameObject yellowTamaDanmaku;

    void Start()
    {
        skySakuraDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/SkySakuraDanmaku");
        yellowSakuraDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/YellowSakuraDanmaku");
        skyTamaDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/SkyTamaDanmaku");
        yellowTamaDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/YellowTamaDanmaku");
    }

    public void StartCard()
    	=> StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<BackgroundManager>().SetBackground(0);
        yield return new WaitForSeconds(3f);

        
        for(int cnt = 0; cnt < 2; cnt++)
        {
    		StartCoroutine(ShootSpiral1(skySakuraDanmaku, 90f, 450f, 25));
            for(int i = 0; i < 4; i++)
            {
            	float startAngle = Random.Range(0f, 360f);
	            StartCoroutine(ShootSpiral2(yellowTamaDanmaku, startAngle, startAngle + 360f, 16));
            }
            yield return new WaitForSeconds(3f);

            GetComponent<SaigyoujiYuyuko>().RandomMove();
    		StartCoroutine(ShootSpiral1(yellowSakuraDanmaku, 90f, 450f, 25));
    		ShootSpirals3(skyTamaDanmaku, 6);
    		yield return new WaitForSeconds(2f);
            
            GetComponent<SaigyoujiYuyuko>().RandomMove();
            yield return new WaitForSeconds(1f);

            for(int i = 0; i < 2; i++)
            {
            	StartCoroutine(ShootSpiral1(yellowSakuraDanmaku, 450f, 90f, 20));
            	yield return new WaitForSeconds(0.8f);
            	StartCoroutine(ShootSpiral4(skyTamaDanmaku, 90f, 450f, 10));
	            yield return new WaitForSeconds(0.8f);
	            StartCoroutine(ShootSpiral1(skySakuraDanmaku, 90f, 450f, 20));
	            yield return new WaitForSeconds(0.8f);
	            StartCoroutine(ShootSpiral4(yellowTamaDanmaku, 450f, 90f, 10));
	            yield return new WaitForSeconds(0.8f);
            }

            if(cnt != 1)
            {
                yield return new WaitForSeconds(3.1f);
                GetComponent<SaigyoujiYuyuko>().RandomMove();
                yield return new WaitForSeconds(1.1f);
            }
        }

        yield return new WaitForSeconds(4f);
        GetComponent<Card1>().StartCard();
        Destroy(this);
    }

    

    IEnumerator ShootSpiral1(GameObject danmaku, float startAngle, float endAngle, int count)
    {
        for(int i = 0; i < count; i++)
        {
            float angle = startAngle + (endAngle - startAngle) * i / count;
            StartCoroutine(ShootLine1(danmaku, angle));
            yield return new WaitForSeconds(0.04f);
        }
    }

    IEnumerator ShootLine1(GameObject danmaku, float angle)
    {
        float startPosition = 5f;
        float endPosition = 120f;
        float deltaPosition = 12f;
        float deltaAngle = 4f;
        for(float position = startPosition; position < endPosition; position += deltaPosition)
        {
            angle += deltaAngle;
            ShootDanmakus1(danmaku, angle, position);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void ShootDanmakus1(GameObject danmaku, float angle, float position)
    {
        int count = 3;

        float alpha = angle * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0f);

        Vector3 targetPosition = transform.position;
        targetPosition += direction * position;
        targetPosition.z = 0f;
        
        Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, targetPosition - transform.position, 40f);
    }

    IEnumerator ShootSpiral2(GameObject danmaku, float startAngle, float endAngle, int count)
    {
    	float position = Random.Range(1f, 100f);
        for(int i = count - 1; i >= 0; i--)
        {
            float angle = startAngle + (endAngle - startAngle) * i / count;
            ShootDanmakus2(danmaku, angle, position);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void ShootDanmakus2(GameObject danmaku, float angle, float position)
    {
        float alpha = angle * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0f);

        Vector3 targetPosition = transform.position;
        targetPosition.z = 0f;
        targetPosition += direction * position;

        Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, targetPosition - transform.position, 36.36f);
    }

    void ShootSpirals3(GameObject danmaku, int count)
    {
    	float deltaAngle = 360f / count;
    	float startAngle = Random.Range(0f, deltaAngle);
    	for(int i = 0; i < count; i++)
    	{
    		float angle = startAngle + deltaAngle * i;
    		StartCoroutine(ShootSpiral3(danmaku, angle, angle + 180f, 8));
    	}
    }

    IEnumerator ShootSpiral3(GameObject danmaku, float startAngle, float endAngle, int count)
    {
    	float startPosition = 10f;
    	float endPosition = 70f;
        for(int i = count - 1; i >= 0; i--)
        {
            float angle = startAngle + (endAngle - startAngle) * i / count;
            float position = startPosition + (endPosition - startPosition) * i / count;
            ShootDanmakus3(danmaku, angle, position);
            yield return new WaitForSeconds(0.08f);
        }
    }

    void ShootDanmakus3(GameObject danmaku, float angle, float position)
    {
        int count = 3;

        float alpha = angle * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0f);

        Vector3 targetPosition = transform.position;
        targetPosition.z = 0f;
        targetPosition += direction * position;

        Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, targetPosition - transform.position, 36.36f);
    }

    IEnumerator ShootSpiral4(GameObject danmaku, float startAngle, float endAngle, int count)
    {
        for(int i = 0; i < count; i++)
        {
            float angle = startAngle + (endAngle - startAngle) * i / count;
            StartCoroutine(ShootLine4(danmaku, angle));
            yield return new WaitForSeconds(0.08f);
        }
    }

    IEnumerator ShootLine4(GameObject danmaku, float angle)
    {
        float startPosition = -4.5f;
        float endPosition = 115.5f;
        float deltaPosition = 40f;
        float deltaAngle = 4f;
        for(float position = startPosition; position < endPosition; position += deltaPosition)
        {
            angle += deltaAngle;
            ShootDanmakus4(danmaku, angle, position);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void ShootDanmakus4(GameObject danmaku, float angle, float position)
    {
        float alpha = angle * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0f);

        Vector3 targetPosition = transform.position;
        targetPosition.z = 0f;
        targetPosition += direction * position;

        Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, targetPosition - transform.position, 40f);
    }
}
