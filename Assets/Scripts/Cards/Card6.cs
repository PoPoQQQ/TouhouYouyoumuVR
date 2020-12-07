using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card6 : MonoBehaviour
{
    GameObject skyLaser;
    GameObject pinkLaser;
    GameObject blueButterflyDanmaku;
    GameObject purpleButterflyDanmaku;
    GameObject redButterflyDanmaku;
    GameObject redOodamaDanmaku;

    void Start()
    {
        skyLaser = Resources.Load<GameObject>("Prefabs/Danmaku/SkyLaser");
        pinkLaser = Resources.Load<GameObject>("Prefabs/Danmaku/PinkLaser");
        blueButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/BlueButterflyDanmaku");
        purpleButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/PurpleButterflyDanmaku");
        redButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/RedButterflyDanmaku");
        redOodamaDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/RedOodamaDanmaku");
    }

    public void StartCard()
    	=> StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.06f, 0.2f);
        yield return new WaitForSeconds(0.06f);
        DanmakuManager.ClearDanmaku();
        GetComponent<SaigyoujiYuyuko>().petals2.Stop();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<SaigyoujiYuyuko>().Oogi(false);
        GetComponent<SaigyoujiYuyuko>().Ring(false);
        GetComponent<BackgroundManager>().SetBackground(5);
        GameObject.Find("Player").GetComponentInChildren<AudioManager>().StopBGM();
        yield return new WaitForSeconds(4f);

        GetComponent<CardEffectManager>().WordsAppear();
        yield return new WaitForSeconds(6f);

        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.5f, 0.5f);
        GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 0f, 100f));
        yield return new WaitForSeconds(0.5f);
        GetComponent<SaigyoujiYuyuko>().Ring(true);
        GetComponent<CardEffectManager>().StartCard("「反魂蝶 -参分咲-」", true);
        GetComponent<BackgroundManager>().SetBackground(1);
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("Player").GetComponentInChildren<AudioManager>().PlayBGM2();
        yield return new WaitForSeconds(3f);

        for(int cnt = 3; cnt < 11; cnt++)
        {
            CreateLasers(cnt);
            StartCoroutine(CreateSpheres1());
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(CreateSpheres2());
            yield return new WaitForSeconds(2.5f);
            CreateSphere3();
            yield return new WaitForSeconds(2f);
        }
        CreateLasers(11);
        StartCoroutine(CreateSpheres1());
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2f);
        GetComponent<WhiteScreenManager>().Flash(Color.white, 1f, 0.5f);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        GetComponent<SaigyoujiYuyuko>().Ring(false);
        GetComponent<SaigyoujiYuyuko>().Tamashi(false);
        GetComponent<BackgroundManager>().SetBackground(5);

        yield return new WaitForSeconds(4f);
        Destroy(this);
    }

    void CreateLasers(int cnt)
    {
        int circles = 7;
        float rotatingAngle = 60f;

        for (int i = 0; i <= circles / 2; i++)
        {
            float angle = 180f / circles * (i + 0.5f);
            for (int j = 0; j < cnt; j++)
            {
                float _angle = 360f / cnt * (j + i * 0.25f - (cnt % 2 == 0? 0.25f: 0f)) - rotatingAngle;
                StartCoroutine(CreateLaser(pinkLaser, angle, _angle, rotatingAngle));
            }
            for (int j = 0; j < cnt; j++)
            {
                float _angle = 180f - 360f / cnt * (j - i * 0.25f - (cnt % 2 == 0? 0.25f: 0f)) + rotatingAngle;
                StartCoroutine(CreateLaser(skyLaser, angle, _angle, -rotatingAngle));
            }
        }
    }

    IEnumerator CreateLaser(GameObject danmaku, float angle1, float angle2, float deltaAngle)
    {
        float duration = 0.8f;

        GameObject laser = Instantiate(danmaku);
        laser.transform.position = transform.position;
        laser.transform.rotation = Quaternion.AngleAxis(angle2, Vector3.forward) * Quaternion.AngleAxis(-angle1, Vector3.up) * Quaternion.AngleAxis(-90f, Vector3.right) * Quaternion.identity;
        laser.AddComponent<LaserDanmaku>().Initialize(duration + 0.2f, 4f);
    
        Quaternion rotation = laser.transform.rotation;
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            float rate = t / duration;
            rate = -rate * rate + 2 * rate;
            //rate = Mathf.Pow(rate, 0.2f);
            laser.transform.rotation = Quaternion.AngleAxis(rate * deltaAngle, Vector3.forward) * rotation;
            yield return 0;
        }

        yield return new WaitForSeconds(3f);

        laser.GetComponent<LaserDanmaku>().Destruct(1f);
    }

    IEnumerator CreateSpheres1()
    {
        for(int cnt = 0; cnt < 1; cnt++)
        {
            CreateSphere1_1();
            yield return new WaitForSeconds(0.1f);
            CreateSphere1_2();
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<SaigyoujiYuyuko>().Tamashi();
    }

    void CreateSphere1_1()
    {
        int circles = 4;
        int[] cnt = new int[] {1, 6, 12, 18, 24};
        float speed = 30f;
        for (int i = 0; i <= circles; i++)
        {
            float angle = 75f / circles * i;
            for (int j = 0; j < cnt[i]; j++)
            {
                float _angle = 360f / cnt[i] * (j + (i % 2 == 0 ? 0.5f : 0f));
                for(int k = 0; k < 3; k++)
                {
                    float _speed = k * 15f + speed;
                    Instantiate(blueButterflyDanmaku).AddComponent<ButterflyDanmaku2>().Initialize(
                        transform.position, Mathf.Sin(angle * Mathf.Deg2Rad) * speed, Mathf.Cos(angle * Mathf.Deg2Rad) * speed, _angle, 0f, 1.5f, _speed);
                }
                
            }
        }
    }

    void CreateSphere1_2()
    {
        int circles = 4;
        int[] cnt = new int[] {6, 12, 18, 24};
        float speed = 30f;
        for (int i = 0; i < circles; i++)
        {
            float angle = 75f / circles * (i + 0.5f);
            for (int j = 0; j < cnt[i]; j++)
            {
                float _angle = 360f / cnt[i] * (j + (i % 2 == 0 ? 0.25f : -0.25f));
                for(int k = 0; k < 3; k++)
                {
                    float _speed = k * 15f + speed;
                    Instantiate(purpleButterflyDanmaku).AddComponent<ButterflyDanmaku2>().Initialize(
                        transform.position, Mathf.Sin(angle * Mathf.Deg2Rad) * speed, Mathf.Cos(angle * Mathf.Deg2Rad) * speed, _angle, 0f, 1.5f, _speed);
                }
                
            }
        }
    }

    IEnumerator CreateSpheres2()
    {
        for(int cnt = 0; cnt < 2; cnt++)
        {
            CreateSphere2(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void CreateSphere2(bool reverse)
    {
        int circles = 4;
        int[] cnt = new int[] {1, 6, 12, 18, 24};
        float speed = 30f;
        for (int i = 0; i <= circles; i++)
        {
            float angle = 75f / circles * i;
            for (int j = 0; j < cnt[i]; j++)
            {
                float _angle = 360f / cnt[i] * (j + (i % 2 == 0 ? 0.5f : 0f));
                for(int k = 0; k < 3; k++)
                {
                    float _speed = k * 15f + speed;
                    Instantiate(redButterflyDanmaku).AddComponent<ButterflyDanmaku2>().Initialize(
                        transform.position, Mathf.Sin(angle * Mathf.Deg2Rad) * speed, Mathf.Cos(angle * Mathf.Deg2Rad) * speed, 
                        _angle, ((i % 2) * 2 - 1) * (reverse ? -1 : 1) * 45f, 1.5f, _speed);
                }
                
            }
        }
    }

    void CreateSphere3()
    {
        int circles = 4;
        int[] cnt = new int[] {6, 12, 18, 24};
        float speed = 30f;
        for (int i = 0; i < circles; i++)
        {
            float angle = 75f / circles * (i + 0.5f);
            for (int j = 0; j < cnt[i]; j++)
            {
                float _angle = 360f / cnt[i] * (j + (i % 2 == 0 ? 0.5f : 0f));
                for(int k = 0; k < 3; k++)
                {
                    float _speed = k * 15f + speed;
                    Instantiate(redOodamaDanmaku).AddComponent<ButterflyDanmaku2>().Initialize(
                        transform.position, Mathf.Sin(angle * Mathf.Deg2Rad) * speed, Mathf.Cos(angle * Mathf.Deg2Rad) * speed, _angle, 0f, 1.5f, _speed);
                }
                
            }
        }
    }

}
