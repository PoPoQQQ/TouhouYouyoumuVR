using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card4 : MonoBehaviour
{
    GameObject greenButterflyObject;
    GameObject blueButterflyObject;
    GameObject yellowButterflyObject;
    GameObject purpleButterflyObject;
    GameObject skyButterflyObject;
    void Start()
    {

        greenButterflyObject = Resources.Load<GameObject>("Prefabs/Danmaku/GreenButterflyDanmaku");
        blueButterflyObject = Resources.Load<GameObject>("Prefabs/Danmaku/BlueButterflyDanmaku");
        yellowButterflyObject = Resources.Load<GameObject>("Prefabs/Danmaku/YellowButterflyDanmaku");
        purpleButterflyObject = Resources.Load<GameObject>("Prefabs/Danmaku/PurpleButterflyDanmaku");
        skyButterflyObject = Resources.Load<GameObject>("Prefabs/Danmaku/SkyButterflyDanmaku");
    }
    public void StartCard()
    	=> StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.5f, 0.5f);
        GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 0f, 100f));
        yield return new WaitForSeconds(0.5f);
        GetComponent<SaigyoujiYuyuko>().Ring(true);
        GetComponent<CardEffectManager>().StartCard("幽曲 「埋骨于弘川 -亡霊-」");
        GetComponent<BackgroundManager>().SetBackground(1);
        yield return new WaitForSeconds(3f);

        int num = 5;
        StartCoroutine(ShootRings(num));
        StartCoroutine(ShootHalfRings(num));
        yield return new WaitForSeconds(num * 6.5f);

        yield return new WaitForSeconds(4f);
        GetComponent<Card5>().StartCard();
        Destroy(this);
    }

    IEnumerator ShootRings(int num)
    {
        for (int I = 0; I < num; I++)
        {
            ShootRing(skyButterflyObject, new Vector3(0f, 0f, 100f), 1, 14);
            ShootRing(skyButterflyObject, new Vector3(0f, 0f, 100f), -1, 14);
            ShootRing(greenButterflyObject, new Vector3(15f, -10f, 100f), -1, 10);
            ShootRing(greenButterflyObject, new Vector3(-15f, -10f, 100f), 1, 10);
            ShootRing(yellowButterflyObject, new Vector3(30f, 10f, 100f), 1, 10);
            ShootRing(yellowButterflyObject, new Vector3(-30f, 10f, 100f), -1, 10);
            yield return new WaitForSeconds(6.5f);
        }
    }

    void ShootRing(GameObject danmaku, Vector3 position, int direction, int count) {
        float r = 20f;
        float r2 = 40f;
        for (int i = 0; i < count; i++)
        {
            float a = Mathf.PI * 2 / count * i;
            Vector3 dest = position;
            dest.z = 0;
            float a2 = a + Mathf.PI / 2 * direction;
            float a3 = a2 + Mathf.PI / 4 * direction;
            dest.x += r2 * Mathf.Cos(a3);
            dest.y += r2 * Mathf.Sin(a3);
            for (int j = 0; j < 12; j += 2)
            {
                GameObject instance = Instantiate(danmaku);
                instance.AddComponent<SpiralRayDanmaku>().InitializeWithDestination(position,
                                                                                  a,
                                                                                  a2,
                                                                                  r,
                                                                                  80,
                                                                                  2f,
                                                                                  25f + j * j / 12 * 2f,
                                                                                  dest);
            }
        }
    }

    IEnumerator ShootHalfRings(int num)
    {
        float movingRange = 20f;
        for (int I = 0; I < num; I++)
        {
            yield return new WaitForSeconds(1f);
            GetComponent<SaigyoujiYuyuko>().MoveTo(
                new Vector3(Random.Range(-movingRange, movingRange), Random.Range(-movingRange, movingRange), 100f));
            yield return new WaitForSeconds(1f);
            ShootHalfRing(blueButterflyObject, Mathf.PI * 1 / 4, Mathf.PI * 3 / 4 / 2, -1);

            GetComponent<SaigyoujiYuyuko>().MoveTo(
                new Vector3(Random.Range(-movingRange, movingRange), Random.Range(-movingRange, movingRange), 100f));
            yield return new WaitForSeconds(1f);
            ShootHalfRing(blueButterflyObject, Mathf.PI * 3 / 4, Mathf.PI * 3 / 4 / 2, 1);

            GetComponent<SaigyoujiYuyuko>().MoveTo(
                new Vector3(Random.Range(-movingRange, movingRange), Random.Range(-movingRange, movingRange), 100f));
            yield return new WaitForSeconds(1f);
            ShootHalfRing(purpleButterflyObject, Mathf.PI * 1 / 4, Mathf.PI * 5 / 4 / 2, -1);

            GetComponent<SaigyoujiYuyuko>().MoveTo(
                new Vector3(Random.Range(-movingRange / 2, movingRange / 2), Random.Range(-movingRange / 2, movingRange / 2), 100f));
            yield return new WaitForSeconds(1f);
            ShootHalfRing(blueButterflyObject, Mathf.PI * 3 / 4, Mathf.PI * 5 / 4 / 2, 1);


            yield return new WaitForSeconds(1f);
            GetComponent<SaigyoujiYuyuko>().MoveTo(
                new Vector3(0, 0, 100f), .5f);
            yield return new WaitForSeconds(.5f);
        }
    }
    void ShootHalfRing(GameObject danmaku, float center, float width, int direction) {
        float dif = Mathf.PI / 16;
        float r = 40f;
        float finalCenter;
        if (direction == -1)
        {
            finalCenter = -Mathf.PI / 4;
        }
        else
        {
            finalCenter = Mathf.PI * 5 / 4;
        }
        for (float a = - width; a <= width; a += dif)
        {
            for (int i = 0; i < 8; i += 2)
            {
                GameObject instance = Instantiate(danmaku);
                instance.AddComponent<SpiralRayDanmaku>().InitializeTowardsPlayer(transform.position,
                                                                                  center + a,
                                                                                  finalCenter + a,
                                                                                  r,
                                                                                  80,
                                                                                  1f,
                                                                                  20f + i * 2f);
            }
        }
    }
}
