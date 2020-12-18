using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCard3 : MonoBehaviour
{
    GameObject shroudObject;
    GameObject skyKunaiDanmaku;
    GameObject blueOodamaDanmaku;

    void Start()
    {
        shroudObject = Resources.Load<GameObject>("Prefabs/Shroud");
        skyKunaiDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/SkyKunaiDanmaku");
        blueOodamaDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/BlueOodamaDanmaku");
    }

    public void StartCard()
    	=> StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.5f, 0.5f);
        GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 10f, 100f));
        yield return new WaitForSeconds(0.5f);
        GetComponent<CardEffectManager>().EndCard();
        GetComponent<SaigyoujiYuyuko>().Ring(false);
        GetComponent<BackgroundManager>().SetBackground(3);

        List<Coroutine> cs = new List<Coroutine>();
        cs.Add(StartCoroutine(ShootKunais(new Vector3(40f, 10f, 120f))));
        cs.Add(StartCoroutine(ShootKunais(new Vector3(-40f, 10f, 120f))));
        cs.Add(StartCoroutine(ReleaseShroud(new Vector3(-40f, 8f, 89f))));
        cs.Add(StartCoroutine(ReleaseShroud(new Vector3(40f, 8f, 89f))));
        cs.Add(StartCoroutine(ShootHemispheres()));
        //yield return new WaitUntil(() => GameObject.Find("Player").GetComponentInChildren<AudioManager>().BGMTime() > 210f);
        yield return new WaitForSeconds(48f);

        foreach (Coroutine c in cs)
        {
            StopCoroutine(c);
        }

        yield return new WaitForSeconds(4f);
        GetComponent<Card3>().StartCard();
        Destroy(this);
    }
    IEnumerator ReleaseShroud(Vector3 initialPosition)
    {
        while (true)
        {
            GameObject shroud = Instantiate(shroudObject);
            float angel = Random.Range(0f, 2 * Mathf.PI);
            shroud.transform.Rotate(new Vector3(0f, 0f, 1f), Random.Range(0f, 360f));
            shroud.transform.position = initialPosition + new Vector3(Mathf.Cos(angel), Mathf.Sin(angel), 0) * Random.Range(1f, 3f);
            yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
        }
    }
    IEnumerator ShootKunais(Vector3 initialPosition)
    {
        
        for (int i = 0; i < 50; i++)
        {
            StartCoroutine(ShootQuadKunai(initialPosition, Random.Range(0f, 360f), skyKunaiDanmaku));
            StartCoroutine(ShootQuadKunai(initialPosition, Random.Range(0f, 360f), skyKunaiDanmaku));
            yield return new WaitForSeconds(0.4f);
            int tot = 10;
            for (int j = 0; j < tot; j++)
            {
                float angle = j * 360f / tot + Random.Range(-180f / tot, 180f / tot);
                StartCoroutine(ShootQuadKunai(initialPosition, angle, skyKunaiDanmaku));
            }
            yield return new WaitForSeconds(0.8f);
        }
        yield break;
    }
    IEnumerator ShootQuadKunai(Vector3 initialPosition, float angle, GameObject danmaku)
    {
        float turningTime = Random.Range(0.7f, 1.7f);
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * 50f - initialPosition;
        Vector3 turningCenter = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        turningCenter *= Random.Range(1f, 3f);
        initialPosition.z -= 0.5f;
        for (int i = 0; i < 4; i++)
        {
            ShootKunai(initialPosition, direction, turningTime, turningCenter, danmaku);
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }
    void ShootKunai(Vector3 initialPosition, Vector3 direction, float turningTime, Vector3 turningCenter, GameObject danmaku)
    {
        GameObject instance = Instantiate(danmaku);
        instance.AddComponent<TurningDanmaku>().InitializeWithRay(initialPosition, direction, 50f, turningTime, turningCenter);
    }
    IEnumerator ShootHemispheres()
    {
        float interval = 3f;
        while (true)
        {
            yield return new WaitForSeconds(interval);
            ShootHemisphere(blueOodamaDanmaku, transform.position);
        }
    }

    void ShootHemisphere(GameObject danmaku, Vector3 position)
    {
        Vector3 z = (GameObject.Find("Player").transform.position - position).normalized;
        Vector3 x = Vector3.ProjectOnPlane(Vector3.right, z).normalized;
        Vector3 y = Vector3.Cross(x, z);
        /*
        for(float rotationX = -90f; rotationX <= 90f; rotationX += 20f)
        {
            Vector3 zx = Quaternion.AngleAxis(rotationX, x) * z;
            for(float rotationY = -90f; rotationY <= 90f; rotationY += 20f)
            {
                Vector3 zy = Quaternion.AngleAxis(rotationY, y) * z;
                ShootDanmaku(danmaku, position, zx + zy - z);
            }
        }
        */
        int circles = 4;
        int[] cnt = new int[] {1, 6, 12, 18, 24};
        for(int i = 0; i <= circles; i++)
        {
            float angle = 75f / circles * i;
            Vector3 u = Quaternion.AngleAxis(angle, x) * z;
            for(int j = 0; j < cnt[i]; j++)
            {
                float _angle = 360f / cnt[i] * (j + (i % 2 == 0 ? 0.5f : 0f));
                Vector3 v = Quaternion.AngleAxis(_angle, z) * u;
                ShootDanmaku(danmaku, transform.position, v);
            }
        }
    }

    void ShootDanmaku(GameObject danmaku, Vector3 position, Vector3 direction)
    {
        Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(position, direction, 20f);
    }
}
