using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card1 : MonoBehaviour
{
    public void StartCard()
    	=> StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.5f, 0.5f);
        GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 10f, 100f));
        yield return new WaitForSeconds(0.5f);
        GetComponent<CardEffectManager>().StartCard("亡郷 「亡我郷 -宿罪-」");
        GetComponent<BackgroundManager>().SetBackground(1);
        yield return new WaitForSeconds(3f);

        GameObject skyScaleDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/SkyScaleDanmaku");
        GameObject yellowScaleDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/YellowScaleDanmaku");
        GameObject greenScaleDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/GreenScaleDanmaku");
        GameObject redLaser = Resources.Load<GameObject>("Prefabs/Danmaku/RedLaser");
        GameObject pinkLaser = Resources.Load<GameObject>("Prefabs/Danmaku/PinkLaser");

        for(int cnt = 0; cnt < 3; cnt++)
        {
            CreateSineWaves(skyScaleDanmaku, yellowScaleDanmaku, greenScaleDanmaku, 1f);
            StartCoroutine(CreateLasers(redLaser, 1f));
    		yield return new WaitForSeconds(8f);
            if(cnt == 2)
                break;
            CreateSineWaves(skyScaleDanmaku, greenScaleDanmaku, yellowScaleDanmaku, -1f);
            StartCoroutine(CreateLasers(pinkLaser, -1f));
            yield return new WaitForSeconds(8f);
        }
        
        yield return new WaitForSeconds(4f);
        GetComponent<NonCard2>().StartCard();
        Destroy(this);
    }

    void CreateSineWaves(GameObject danmaku1, GameObject danmaku2, GameObject danmaku3, float negative)
    {
        bool flag = false;
        float initial = Random.Range(-12.5f, 12.5f);
        for(float x = -50f + initial; x <= 55f + initial; x += 25f)
        {
            StartCoroutine(CreateSineWave(flag? danmaku2: danmaku1, new Vector3(x, -40f * negative, 0f), negative));
            flag = !flag; 
        }

        initial = Random.Range(-10f, 10f);
        for(float x = -25f + initial; x <= 30f + initial; x += 25f)
        {
            StartCoroutine(CreateSineWave(flag? danmaku3: danmaku1, new Vector3(x, 40f * negative, 0f), -negative));
            flag = !flag; 
        }
    }

    IEnumerator CreateSineWave(GameObject danmaku, Vector3 eulerAngles, float negative)
    {
        GameObject sineWave = new GameObject();
        Destroy(sineWave, 10f);
        sineWave.transform.position = transform.position + new Vector3(0f, 0f, -1f);
        sineWave.transform.rotation = Quaternion.Euler(eulerAngles);
        StartCoroutine(ShootSineWave(danmaku, sineWave.transform));
        StartCoroutine(RotateSineWave(sineWave, negative));
        yield break;
    }

    IEnumerator ShootSineWave(GameObject danmaku, Transform parent)
    {
        for(float t = Time.time; Time.time < t + 7.5f;)
        {
            ShootDanmaku(danmaku, parent);
            yield return new WaitForSeconds(Random.Range(0.03f, 0.05f));
        }
    }

    IEnumerator RotateSineWave(GameObject sineWave, float negative)
    {
        Vector3 eulerAngles = sineWave.transform.rotation.eulerAngles;
        float duration = 9f;
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            float deltaAngle = -(Time.deltaTime / duration) * negative * 280f;
            eulerAngles.y += deltaAngle;
            sineWave.transform.rotation = Quaternion.Euler(eulerAngles);
            yield return 0;
        }
    }

    void ShootDanmaku(GameObject danmaku, Transform parent)
    {
        GameObject instance = Instantiate(danmaku);
        instance.transform.parent = parent;
        instance.AddComponent<SineWaveDanmaku>().Initialize(Vector3.zero, 50f);
    }

    IEnumerator CreateLasers(GameObject danmaku, float negative)
    {
        yield return new WaitForSeconds(0.15f);
        for(int i = -10; i <= 10; i++)
        {
            StartCoroutine(CreateLaser(danmaku, i * 3.5f, negative));
            //yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator CreateLaser(GameObject danmaku, float deltaRotationZ, float negative)
    {
        GameObject laser = Instantiate(danmaku);
        laser.transform.position = transform.position - new Vector3(0f, 0f, 1f);
        laser.transform.rotation = Quaternion.Euler(0f, 0f, 90f * negative);
        laser.AddComponent<LaserDanmaku>().Initialize(1f, 5f);
        
        yield return new WaitForSeconds(1.15f);

        float duration = 4f;
        StartCoroutine(RotateLaser1(laser, deltaRotationZ, negative, duration));
        StartCoroutine(RotateLaser2(laser, negative, duration));
        StartCoroutine(DestroyLaser(laser, deltaRotationZ, duration));
    }

    IEnumerator RotateLaser1(GameObject laser, float deltaRotationZ, float negative, float duration)
    {
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            float angle = 90f + (-180f + deltaRotationZ) * Mathf.Sqrt(0.5f - 0.5f * Mathf.Cos(t / duration * Mathf.PI));
            Vector3 eulerAngles = laser.transform.eulerAngles;
            eulerAngles.z = angle * negative;
            laser.transform.rotation = Quaternion.Euler(eulerAngles);
            yield return 0;
        }
    }

    IEnumerator RotateLaser2(GameObject laser, float negative, float duration)
    {
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            //float angle = 90f * Mathf.Pow(t / duration, 3f);
            float angle = 90f * Mathf.Pow(0.5f - 0.5f * Mathf.Cos(t / duration * Mathf.PI), 2);
            Vector3 eulerAngles = laser.transform.eulerAngles;
            eulerAngles.y = angle * negative;
            laser.transform.rotation = Quaternion.Euler(eulerAngles);
            yield return 0;
        }
    }

    IEnumerator DestroyLaser(GameObject laser, float deltaRotationZ, float duration)
    {
        yield return new WaitForSeconds(duration + 0.6f - deltaRotationZ * 0.02f);
        yield return new WaitForSeconds(0.05f);
        laser.GetComponent<LaserDanmaku>().Destruct();
    }
}
