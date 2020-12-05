using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCard4 : MonoBehaviour
{
    GameObject blueScaleObject;
    GameObject greenScaleObject;
    GameObject skyScaleObject;
    GameObject greenButterflyObject;
    void Start()
    {
        blueScaleObject = Resources.Load<GameObject>("Prefabs/Danmaku/BlueScaleDanmaku");
        greenScaleObject = Resources.Load<GameObject>("Prefabs/Danmaku/GreenScaleDanmaku");
        skyScaleObject = Resources.Load<GameObject>("Prefabs/Danmaku/SkyScaleDanmaku");
        greenButterflyObject = Resources.Load<GameObject>("Prefabs/Danmaku/GreenButterflyDanmaku");
    }
    public void StartCard()
        => StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.5f, 0.5f);
        GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 10f, 100f));
        yield return new WaitForSeconds(0.5f);
        GetComponent<SaigyoujiYuyuko>().Oogi(false);
        GetComponent<SaigyoujiYuyuko>().Ring(false);
        GetComponent<BackgroundManager>().SetBackground(4);
        yield return new WaitForSeconds(3f);

        int num = 5;
        StartCoroutine(ShootWhip(num));
        StartCoroutine(MoveYuyuko(num * 4));
        StartCoroutine(ShootHemispheres(num * 4));
        yield return new WaitForSeconds(num * 8f);


        GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 0f, 100f));

        yield return new WaitForSeconds(5f);

        yield return new WaitForSeconds(4f);
        GetComponent<Card4>().StartCard();
        Destroy(this);
    }
    IEnumerator ShootWhip(int num)
    {
        for (int I = 0; I < num; I++)
        {
            StartCoroutine(Whip(1f, blueScaleObject));
            StartCoroutine(Whip(1f, blueScaleObject));
            yield return new WaitForSeconds(2f);
            StartCoroutine(Whip(10f, skyScaleObject));
            StartCoroutine(Whip(10f, skyScaleObject));
            StartCoroutine(Whip(10f, skyScaleObject));
            yield return new WaitForSeconds(2f);
            StartCoroutine(Whip(20f, greenScaleObject));
            StartCoroutine(Whip(20f, greenScaleObject));
            StartCoroutine(Whip(20f, greenScaleObject));
            StartCoroutine(Whip(1f, blueScaleObject));
            StartCoroutine(Whip(1f, blueScaleObject));
            yield return new WaitForSeconds(2f);
            StartCoroutine(Whip(30f, skyScaleObject));
            StartCoroutine(Whip(30f, skyScaleObject));
            StartCoroutine(Whip(10f, skyScaleObject));
            StartCoroutine(Whip(10f, skyScaleObject));
            yield return new WaitForSeconds(2f);

        }
    }
    IEnumerator Whip(float finalRadius, GameObject danmakuObject)
    {
        float turningSquareWidth = 25f;
        Vector3 turningPosition = new Vector3(
            Random.Range(-turningSquareWidth, turningSquareWidth), Random.Range(-turningSquareWidth, turningSquareWidth), 0f);
        turningPosition += gameObject.transform.position;
        turningPosition.z = 50f;
        float t1 = Random.Range(0, Mathf.PI * 2);
        float t2 = Random.Range(0, Mathf.PI * 2);
        Vector3 turningOffset = new Vector3(Mathf.Cos(t1), Mathf.Sin(t1), 0);
        Vector3 finalOffset = new Vector3(Mathf.Cos(t2), Mathf.Sin(t2), 0);
        WhipDanmaku tip = null;
        for (int i = 0; i < 20; i++)
        {
            Vector3 initialPosition = gameObject.transform.position;
            GameObject instance = Instantiate(danmakuObject);
            WhipDanmaku whip = instance.AddComponent<WhipDanmaku>();
            if (tip == null)
            {
                tip = whip;
            }
            whip.InitializeWithPoints(
                initialPosition, turningPosition + i * 1.5f * turningOffset, finalOffset * (finalRadius + i * 3f), tip, 50f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator MoveYuyuko(int num)
    {
        float movingRange = 25f;
        for (int i = 0; i < num; i++)
        {
            GetComponent<SaigyoujiYuyuko>().MoveTo(
                new Vector3(Random.Range(-movingRange, movingRange), Random.Range(-movingRange, movingRange), 100f));
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator ShootHemispheres(int num)
    {
        float interval = 2f;
        for (int i = 0; i < num; i++)
        {
            {
                ShootHemisphere(greenButterflyObject, transform.position, 30f);
                ShootHemisphere(greenButterflyObject, transform.position, 25f);
                ShootHemisphere(greenButterflyObject, transform.position, 20f);
                yield return new WaitForSeconds(interval);
            }
        }

        void ShootHemisphere(GameObject danmaku, Vector3 position, float speed)
        {
            Vector3 z = (GameObject.Find("Player").transform.position - position).normalized;
            Vector3 x = Vector3.ProjectOnPlane(Vector3.right, z).normalized;
            Vector3 y = Vector3.Cross(x, z);
            int circles = 4;
            int[] cnt = new int[] { 1, 6, 12, 18, 24 };
            for (int i = 0; i <= circles; i++)
            {
                float angle = 75f / circles * i;
                Vector3 u = Quaternion.AngleAxis(angle, x) * z;
                for (int j = 0; j < cnt[i]; j++)
                {
                    float _angle = 360f / cnt[i] * (j + (i % 2 == 0 ? 0.5f : 0f));
                    Vector3 v = Quaternion.AngleAxis(_angle, z) * u;
                    ShootDanmaku(danmaku, transform.position, v, speed);
                }
            }
        }

        void ShootDanmaku(GameObject danmaku, Vector3 position, Vector3 direction, float speed)
        {
            Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithRay(position, direction, speed);
        }
    }
}