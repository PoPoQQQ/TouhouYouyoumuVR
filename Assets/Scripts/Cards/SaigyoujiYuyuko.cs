using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaigyoujiYuyuko : MonoBehaviour
{
	Transform player;

    public GameObject oogi;
    public GameObject ring;

    void Start()
    {
    	player = GameObject.Find("Player").transform;
    	StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        {
            Color color = spriteRenderer.color;
            color.a = 0f;
            spriteRenderer.color = color;
        }
        yield return new WaitForSeconds(1f);
        float duration = 2f;
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = t / duration;
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
            yield return 0;
        }
        {
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
        yield return new WaitForSeconds(1f);
        GameObject.Find("Player").GetComponentInChildren<AudioManager>().PlayBGM();
        GetComponent<NonCard1>().StartCard();
        //StartCoroutine(TestingCard());
    }

    public void RandomMove()
    {
        Vector3 target;
        do {
            target = Random.insideUnitCircle * 25f;
            target.z = transform.position.z;
        } while((target - transform.position).magnitude < 20f);
        MoveTo(target);
    }

    public void MoveTo(Vector3 target)
    {
        GetComponent<SpriteRenderer>().flipX = (target.x < transform.position.x);
        GetComponent<Animator>().SetTrigger("Dash");
        StartCoroutine(MoveToCoroutine(target));
    }

    IEnumerator MoveToCoroutine(Vector3 target)
    {
        Vector3 deltaPosition = target - transform.position;
        float duration = 1f;
        Vector3 originPosition = transform.position;
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            float rate = 0.5f - Mathf.Cos((t / duration) * Mathf.PI) * 0.5f;
            rate = rate * rate;
            transform.position = originPosition * (1 - rate) + target * rate;
            yield return 0;
        }
        transform.position = target;
    }

    public void Oogi(bool enabled)
    {
        if(enabled)
            StartCoroutine(OogiCoroutine());
        else
            oogi.transform.localScale = new Vector3(0f, 0f, 1);
    }

    IEnumerator OogiCoroutine()
    {
        float duration = 0.1f;
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            oogi.transform.localScale = new Vector3(t / duration, t / duration * 0.2f, 1f);
            yield return 0;
        }

        yield return new WaitForSeconds(0.05f);
        GetComponent<BackgroundManager>().SetBackground(2);
        duration = 0.3f;
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            oogi.transform.localScale = new Vector3(1f, 0.2f + t / duration * 0.8f, 1f);
            yield return 0;
        }
        oogi.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Ring(bool enabled)
        => ring.SetActive(enabled);

    IEnumerator TestingCard()
    {
    	GameObject blueDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/RedBallDanmaku");
    	GameObject redDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/SkyButterflyDanmaku");
    	bool left = true;
    	float alpha = 0f;
    	while(true)
    	{
    		for(int i = 0; i < 3; i++)
        		Instantiate(left? blueDanmaku: redDanmaku).AddComponent<LinearDanmaku>().InitializeWithSegment(
        				new Vector3(transform.position.x + (left? -1: 1) * 35f, transform.position.y, transform.position.z + 0.1f),
        				(Vector3)Random.insideUnitCircle * 100f,
        				4f);
    		Instantiate(left? blueDanmaku: redDanmaku).AddComponent<LinearDanmaku>().InitializeWithSegment(
    				new Vector3(transform.position.x + (left? -1: 1) * 35f, transform.position.y, transform.position.z + 0.1f),
    				player.position,
    				2.5f);
    		Instantiate(left? blueDanmaku: redDanmaku).AddComponent<LinearDanmaku>().InitializeWithSegment(
    				new Vector3(transform.position.x + (left? -1: 1) * 35f, transform.position.y, transform.position.z + 0.1f),
    				player.position + new Vector3(Mathf.Cos(left? -alpha: alpha), Mathf.Sin(left? -alpha: alpha), 0f) * 20f,
    				2.5f);
    		Instantiate(left? blueDanmaku: redDanmaku).AddComponent<LinearDanmaku>().InitializeWithSegment(
    				new Vector3(transform.position.x + (left? -1: 1) * 35f, transform.position.y, transform.position.z + 0.1f),
    				player.position - new Vector3(Mathf.Cos(left? -alpha: alpha), Mathf.Sin(left? -alpha: alpha), 0f) * 20f,
    				2.5f);
    		left = !left;
    		alpha += 0.1f;
    		yield return new WaitForSeconds(0.1f);
    	}
    }
}
