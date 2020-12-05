using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card3 : MonoBehaviour
{
    GameObject blueTamaDanmaku;
    GameObject spirits;
    GameObject skyButterflyDanmaku;
    GameObject blueButterflyDanmaku;
    GameObject purpleButterflyDanmaku;
    GameObject redButterflyDanmaku;

    void Start()
    {
        blueTamaDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/BlueTamaDanmaku");
        spirits = Resources.Load<GameObject>("Prefabs/Spirits");
        skyButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/SkyButterflyDanmaku");
        blueButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/BlueButterflyDanmaku");
        purpleButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/PurpleButterflyDanmaku");
        redButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/RedButterflyDanmaku");
    }

    public void StartCard()
    	=> StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.5f, 0.5f);
        GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 0f, 100f));
        yield return new WaitForSeconds(0.5f);
        GetComponent<SaigyoujiYuyuko>().Ring(true);
        GetComponent<CardEffectManager>().StartCard("華霊 「Swallow Tail Butterfly -燕尾蝶-」");
        GetComponent<BackgroundManager>().SetBackground(1);
        yield return new WaitForSeconds(3f);

        Coroutine c1 = StartCoroutine(BlueTamaCoroutine(blueTamaDanmaku));
        yield return StartCoroutine(ButterflyCoroutine());

        StopCoroutine(c1);
        yield return new WaitForSeconds(4f);
        GetComponent<NonCard4>().StartCard();
        Destroy(this);
    }

    IEnumerator BlueTamaCoroutine(GameObject danmaku)
    {
        float interval = 1f;
        yield return new WaitForSeconds(1f);
        while(true)
        {
            yield return new WaitForSeconds(interval);
            interval = Mathf.Max(interval * 0.96f, 0.5f);
            ShootRings(danmaku);
        }
    }

    void ShootRings(GameObject danmaku)
    {
    	float radius = Random.Range(5f, 95f);
    	int cnt = ((int)(radius * 0.05f) + 1) * 3;
    	ShootRing(danmaku, radius + 5f, cnt, 3f, 0f);
    	ShootRing(danmaku, radius, cnt, 4f, 0.5f);
    	ShootRing(danmaku, radius - 5f, cnt, 5f, 0f);
    }

    void ShootRing(GameObject danmaku, float radius, int cnt, float duration, float offset)
    {
    	for(int i = 0; i < cnt; i++)
    	{
    		float alpha = Mathf.PI * 2 / cnt * (i + offset);
    		Vector3 targetPosition = transform.position + new Vector3(Mathf.Cos(alpha) * radius, Mathf.Sin(alpha) * radius, -100f);
    		Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithSegment(transform.position, targetPosition, duration);
    	}
    }

    IEnumerator ButterflyCoroutine()
    {
    	Vector3 lastPosition = transform.position;
    	for(int cnt = 0; cnt < 14; cnt++)
    	{
    		Vector3 position = GameObject.Find("Player").transform.position;
    		position.z = 50f;
    		Instantiate(spirits).AddComponent<Spirits>().Initialize(lastPosition, position, 2f);
    		yield return new WaitForSeconds(2f);
    		ShootButterflies(position);
    		lastPosition = position;
    		yield return new WaitForSeconds(2f);
    	}
    }

    void ShootButterflies(Vector3 position)
    {
    	for(int i = 0; i < 8; i++)
    	{
    		float angle = i * 45f;
    		float omega = 90f;
    		float zSpeed = 25f;
    		float[] rSpeeds = new float[] {2f, 8f, 14f, 20f};
    		Instantiate(skyButterflyDanmaku).AddComponent<RotatingDanmaku2>().Initialize(position, angle - 5f, -omega, rSpeeds[0], zSpeed);
    		Instantiate(skyButterflyDanmaku).AddComponent<RotatingDanmaku2>().Initialize(position, angle + 5f, -omega, rSpeeds[0], zSpeed);
    		Instantiate(blueButterflyDanmaku).AddComponent<RotatingDanmaku2>().Initialize(position, angle - 5f, omega, rSpeeds[1], zSpeed);
    		Instantiate(blueButterflyDanmaku).AddComponent<RotatingDanmaku2>().Initialize(position, angle + 5f, omega, rSpeeds[1], zSpeed);
    		Instantiate(purpleButterflyDanmaku).AddComponent<RotatingDanmaku2>().Initialize(position, angle - 5f, -omega, rSpeeds[2], zSpeed);
    		Instantiate(purpleButterflyDanmaku).AddComponent<RotatingDanmaku2>().Initialize(position, angle + 5f, -omega, rSpeeds[2], zSpeed);
    		Instantiate(redButterflyDanmaku).AddComponent<RotatingDanmaku2>().Initialize(position, angle - 5f, omega, rSpeeds[3], zSpeed);
    		Instantiate(redButterflyDanmaku).AddComponent<RotatingDanmaku2>().Initialize(position, angle + 5f, omega, rSpeeds[3], zSpeed);
    	}
    }

}
