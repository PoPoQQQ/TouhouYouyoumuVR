using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card2 : MonoBehaviour
{
    public void StartCard()
    	=> StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.5f, 0.5f);
        GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 0f, 100f));
        yield return new WaitForSeconds(0.5f);
        GetComponent<SaigyoujiYuyuko>().Ring(true);
        GetComponent<CardEffectManager>().StartCard("亡舞 「生者必滅之理 -死蝶-」");
        GetComponent<BackgroundManager>().SetBackground(1);
        yield return new WaitForSeconds(3f);

        GameObject blueOodamaDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/BlueOodamaDanmaku");
        GameObject skyButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/SkyButterflyDanmaku");
        GameObject blueButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/BlueButterflyDanmaku");
        GameObject purpleButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/PurpleButterflyDanmaku");

        StartCoroutine(SwitchingCoroutine());
        StartCoroutine(RotatingCoroutine());
        Coroutine c1 = StartCoroutine(CircleCoroutine(blueOodamaDanmaku));
        Coroutine c2 = StartCoroutine(SkyButterflyCoroutine(skyButterflyDanmaku));
        Coroutine c3 = StartCoroutine(BlueButterflyCoroutine(blueButterflyDanmaku));
        Coroutine c4 = StartCoroutine(PurpleButterflyCoroutine(purpleButterflyDanmaku));

        yield return new WaitForSeconds(42f);
        StopCoroutine(c1);
        StopCoroutine(c2);
        StopCoroutine(c3);
        StopCoroutine(c4);

        yield return new WaitForSeconds(7f);
        GetComponent<NonCard3>().StartCard();
        Destroy(this);
    }

    bool flag = false;

    IEnumerator SwitchingCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(10f);
            flag = !flag;
        }
    }

    
    float angle = 0f;
    float deltaAngle = 66.66f;

    IEnumerator RotatingCoroutine()
    {
        while(true)
        {
            angle += deltaAngle * (flag ? -1 : 1) * Time.deltaTime;
            yield return 0;
        }
    }

    IEnumerator CircleCoroutine(GameObject danmaku)
    {
        float interval = 0.15f;
        int count = 0;
        while(true)
        {
            count++;
            if((count % 10) >= 5)
            {
                Vector3 initialPosition = transform.position;
                Vector3 targetPosition = initialPosition + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * 80f;
                Instantiate(danmaku).AddComponent<PathDanmaku>().Initialize(initialPosition, targetPosition, 2f, 40f);
            }
            
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator SkyButterflyCoroutine(GameObject danmaku)
    {
        float interval = 0.3f;
        
        while(true)
        {
            float rho = Mathf.Sin(Time.time * 0.5f) * 50f;
            for(int i = 0; i < 4; i++)
            {
                float angle = this.angle + i * 90f;
                Vector3 initialPosition = transform.position;
                Vector3 target = initialPosition + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * rho, Mathf.Sin(angle * Mathf.Deg2Rad) * rho, -100f);
                Instantiate(danmaku).AddComponent<LinearDanmaku>().InitializeWithSegment(initialPosition, target, 5f);
            }
            yield return new WaitForSeconds(interval);
        }
        
        yield break;
    }

    IEnumerator BlueButterflyCoroutine(GameObject danmaku)
    {
        float interval = 0.3f;
        
        while(true)
        {
            float rho = Mathf.Sin(Time.time * 0.3f) * 50f;
            for(int i = 0; i < 2; i++)
            {
                float angle = this.angle + i * 180f;
                Vector3 initialPosition = transform.position;
                Vector3 target = initialPosition + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * rho, Mathf.Sin(angle * Mathf.Deg2Rad) * rho, 0f);
                Instantiate(danmaku).AddComponent<RotatingDanmaku>().Initialize(initialPosition, target, 2f, 20f, -30f);
            }
            yield return new WaitForSeconds(interval);
        }
        
        yield break;
    }

    IEnumerator PurpleButterflyCoroutine(GameObject danmaku)
    {
        float interval = 0.3f;
        
        while(true)
        {
            float rho = Mathf.Sin(Time.time * 0.4f) * 50f;
            for(int i = 0; i < 2; i++)
            {
                float angle = this.angle + i * 180f;
                Vector3 initialPosition = transform.position;
                Vector3 target = initialPosition + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * rho, Mathf.Sin(angle * Mathf.Deg2Rad) * rho, 0f);
                Instantiate(danmaku).AddComponent<RotatingDanmaku>().Initialize(initialPosition, target, 2f, 20f, 30f);
            }
            yield return new WaitForSeconds(interval);
        }
        
        yield break;
    }

}
