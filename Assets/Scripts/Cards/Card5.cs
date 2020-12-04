using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card5 : MonoBehaviour
{
    GameObject blueOodamaDanmaku;
    GameObject purpleButterflyDanmaku;
    GameObject skyButterflyDanmaku;
    GameObject pinkSakuraDanmaku;

    void Start()
    {
        blueOodamaDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/BlueOodamaDanmaku");
        purpleButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/PurpleButterflyDanmaku");
        skyButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/SkyButterflyDanmaku");
        pinkSakuraDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/PinkSakuraDanmaku");
    }

    public void StartCard()
    	=> StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SaigyoujiYuyuko>().Ring(false);
        GetComponent<BackgroundManager>().SetBackground(4);
        yield return new WaitForSeconds(3f);

        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.5f, 0.5f);
        GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 0f, 100f));
        yield return new WaitForSeconds(0.5f);
        GetComponent<SaigyoujiYuyuko>().Ring(true);
        GetComponent<CardEffectManager>().StartCard("樱符 「完全墨染的樱花 -亡我-」");
        GetComponent<BackgroundManager>().SetBackground(1);
        yield return new WaitForSeconds(3f);

        ShootSphere1(blueOodamaDanmaku);
        yield return new WaitForSeconds(2f);
        GetComponent<SaigyoujiYuyuko>().Oogi(true);
        yield return new WaitForSeconds(3f);

        StartCoroutine(ShootSakura(pinkSakuraDanmaku));
        for(int cnt = 0; cnt < 1; cnt++)
        {
            ShootButterflies(purpleButterflyDanmaku, true);
            yield return new WaitForSeconds(1.5f);
            GetComponent<SaigyoujiYuyuko>().RandomMove();
            yield return new WaitForSeconds(2f);
            ShootButterflies(skyButterflyDanmaku, false);
            yield return new WaitForSeconds(1.5f);
            ShootButterflies(purpleButterflyDanmaku, true);
            yield return new WaitForSeconds(1.5f);
            GetComponent<SaigyoujiYuyuko>().RandomMove();
            yield return new WaitForSeconds(2f);
            ShootButterflies(skyButterflyDanmaku, false);
            ShootSphere2(blueOodamaDanmaku);
            yield return new WaitForSeconds(1.5f);
        }

        GetComponent<SaigyoujiYuyuko>().petals2.Play();
        yield return new WaitForSeconds(4f);
        GetComponent<Card6>().StartCard();
        Destroy(this);
    }

    IEnumerator ShootSakura(GameObject danmaku)
    {
        float radius = 90f;
        while(true)
        {
            float angle = Random.Range(0f, 360f);
            Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius, Random.Range(30f, 100f));
            Vector3 direction = new Vector3(0f, (angle < 90f? -1: 1) * 10f, -5f);
            Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(position, direction, direction.magnitude);
            yield return new WaitForSeconds(0.05f);
        }
    }

    void ShootSphere1(GameObject danmaku)
    {
        Vector3 z = Vector3.back;
        Vector3 x = Vector3.right;

        int circles = 9;
        int[] cnt = new int[] {6, 12, 18, 18, 24, 18, 18, 12, 6};
        for(int i = 0; i < circles; i++)
        {
            float angle = 180f / circles * (i + 0.5f);
            Vector3 u = Quaternion.AngleAxis(angle, x) * z;
            for(int j = 0; j < cnt[i]; j++)
            {
                float _angle = 360f / cnt[i] * (j + (i % 2 == 0 ? 0.5f : 0f));
                Vector3 v = Quaternion.AngleAxis(_angle, z) * u;
                //Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, v, 20f);
                //Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, v, 25f);
                Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, v, 30f);
            }
        }
    }

    void ShootButterflies(GameObject danmaku, bool reverse)
    {
        Vector3 z = Vector3.back;
        Vector3 x = Vector3.right;
        Vector3 y = Quaternion.AngleAxis(40f, x) * z;
        float initialAngle = Random.Range(0f, 360f);
        float __angle = Random.Range(10f, 170f);
        for(int a = 0; a < 2; a++)
        {
            float angle = (a * 2 - 1) * __angle;
            Vector3 u = Quaternion.AngleAxis(angle, z) * y;
            for(int i = 0; i < 6; i++)
            {
                float distance = i * 15f + 5f;
                for(int j = 0; j < 3; j++)
                {
                    float _angle = initialAngle + j * 120f + i * 10f;
                    for(int k = 0; k < 4; k++)
                    {
                        float xySpeed = k * 7.5f + 10f;
                        Instantiate(danmaku).AddComponent<ButterflyDanmaku>().Initialize(transform.position, transform.position + u * distance, _angle, reverse, xySpeed);
                    }
                    
                }
                
            }
        }
    }

    void ShootSphere2(GameObject danmaku)
    {
        Vector3 z = Vector3.back;
        Vector3 x = Vector3.right;

        int circles = 5;
        int[] cnt = new int[] {6, 12, 18, 12, 6};
        for(int i = 0; i < circles; i++)
        {
            float angle = 180f / circles * (i + 0.5f);
            Vector3 u = Quaternion.AngleAxis(angle, x) * z;
            for(int j = 0; j < cnt[i]; j++)
            {
                float _angle = 360f / cnt[i] * (j + (i % 2 == 0 ? 0.5f : 0f));
                Vector3 v = Quaternion.AngleAxis(_angle, z) * u;
                //Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, v, 20f);
                //Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, v, 25f);
                Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(transform.position, v, 30f);
            }
        }
    }

}
