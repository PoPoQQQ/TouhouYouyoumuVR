using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCard1 : MonoBehaviour
{
    public void StartCard()
    	=> StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<BackgroundManager>().SetBackground(0);
        yield return new WaitForSeconds(3f);

        GameObject skySakuraDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/SkySakuraDanmaku");
        GameObject yellowSakuraDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/YellowSakuraDanmaku");
        GameObject skyTamaDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/SkyTamaDanmaku");
        GameObject yellowTamaDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/YellowTamaDanmaku");
        for(int cnt = 0; cnt < 2; cnt++)
        {
    		StartCoroutine(ShootSpiral1(skySakuraDanmaku, 90f, 450f, 25));
            for(int i = 0; i < 4; i++)
            {
            	float startAngle = Random.Range(0f, 360f);
	            StartCoroutine(ShootSpiral2(yellowTamaDanmaku, startAngle, startAngle + 360f, 8));
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
        float startPosition = -5f;
        float endPosition = 25f;
        float deltaPosition = 3f;
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

        Vector3 sourcePosition = transform.position;
        sourcePosition.z -= 0.1f;
        sourcePosition += direction * position;

        Vector3 targetPosition = transform.position;
        targetPosition.z = 0f;
        targetPosition += direction * (4f * position + 15.5f);

        Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithSegment(sourcePosition, targetPosition, 2.5f);
    }

    IEnumerator ShootSpiral2(GameObject danmaku, float startAngle, float endAngle, int count)
    {
    	float position = Random.Range(10f, 25f);
        for(int i = count - 1; i >= 0; i--)
        {
            float angle = startAngle + (endAngle - startAngle) * i / count;
            ShootDanmakus2(danmaku, angle, position);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void ShootDanmakus2(GameObject danmaku, float angle, float position)
    {
        int count = 3;

        float alpha = angle * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0f);

        Vector3 sourcePosition = transform.position;
        sourcePosition.z -= 0.01f;
        sourcePosition += direction * position;

        Vector3 targetPosition = transform.position;
        targetPosition.z = 0f;
        targetPosition += direction * (4f * position - 50f);

        Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithSegment(sourcePosition, targetPosition, 2.75f);
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
    	float startPosition = 20f;
    	float endPosition = 50f;
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

        Vector3 sourcePosition = transform.position;
        sourcePosition.z -= 0.01f;
        sourcePosition += direction * position;

        Vector3 targetPosition = transform.position;
        targetPosition.z = 0f;
        targetPosition += direction * (2f * position - 30f);

        Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithSegment(sourcePosition, targetPosition, 2.75f);
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
        float startPosition = -5f;
        float endPosition = 25f;
        float deltaPosition = 10f;
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
        int count = 3;

        float alpha = angle * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0f);

        Vector3 sourcePosition = transform.position;
        sourcePosition.z -= 0.1f;
        sourcePosition += direction * position;

        Vector3 targetPosition = transform.position;
        targetPosition.z = 0f;
        targetPosition += direction * (4f * position + 15.5f);

        Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithSegment(sourcePosition, targetPosition, 2.5f);
    }
}
