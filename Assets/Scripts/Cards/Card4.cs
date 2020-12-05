using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card4 : MonoBehaviour
{
    GameObject skyButterflyDanmaku;
    GameObject greenButterflyDanmaku;
    GameObject yellowButterflyDanmaku;
    GameObject blueButterflyDanmaku;
    GameObject purpleButterflyDanmaku;

    void Start()
    {
        skyButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/SkyButterflyDanmaku");
        greenButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/GreenButterflyDanmaku");
        yellowButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/YellowButterflyDanmaku");
        blueButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/BlueButterflyDanmaku");
        purpleButterflyDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/PurpleButterflyDanmaku");
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

        for(int cnt = 0; cnt < 8; cnt++)
        {
            ShootRing(skyButterflyDanmaku, new Vector3(0f, 0f, 100f), 14, false);
            ShootRing(skyButterflyDanmaku, new Vector3(0f, 0f, 100f), 14, true);
            ShootRing(greenButterflyDanmaku, new Vector3(-19.3f, -8.1f, 100f), 10, false);
            ShootRing(greenButterflyDanmaku, new Vector3(19.3f, -8.1f, 100f), 10, true);
            ShootRing(yellowButterflyDanmaku, new Vector3(-34.1f, -3.1f, 100f), 10, true);
            ShootRing(yellowButterflyDanmaku, new Vector3(34.1f, -3.1f, 100f), 10, false);
            yield return new WaitForSeconds(1.2f);
            Vector3 target;

            do {
                target = Random.insideUnitCircle * 25f;
                target.z = transform.position.z;
            } while(target.x <= transform.position.x || target.y <= 0);
            GetComponent<SaigyoujiYuyuko>().MoveTo(target, 0.9f);
            yield return new WaitForSeconds(0.9f);
            ShootArc(blueButterflyDanmaku, transform.position, 10, true);

            do {
                target = Random.insideUnitCircle * 25f;
                target.z = transform.position.z;
            } while(target.x >= transform.position.x || target.y <= 0);
            GetComponent<SaigyoujiYuyuko>().MoveTo(target, 0.9f);
            yield return new WaitForSeconds(0.9f);
            ShootArc(blueButterflyDanmaku, transform.position, 12, false);

            do {
                target = Random.insideUnitCircle * 25f;
                target.z = transform.position.z;
            } while(target.x <= transform.position.x || target.y <= 0);
            GetComponent<SaigyoujiYuyuko>().MoveTo(target, 0.9f);
            yield return new WaitForSeconds(0.9f);
            ShootArc(purpleButterflyDanmaku, transform.position, 18, true);

            do {
                target = Random.insideUnitCircle * 25f;
                target.z = transform.position.z;
            } while(target.x >= transform.position.x || target.y <= 0);
            GetComponent<SaigyoujiYuyuko>().MoveTo(target, 0.9f);
            yield return new WaitForSeconds(0.9f);
            ShootArc(blueButterflyDanmaku, transform.position, 16, false);
            
            if(cnt == 7)
                break;
            GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 0f, 100f), 1.2f);
            yield return new WaitForSeconds(1.2f);
        }

        yield return new WaitForSeconds(4f);
        GetComponent<Card5>().StartCard();
        Destroy(this);
    }

    void ShootRing(GameObject danmaku, Vector3 position, int cnt, bool reverse)
    {
        for(int i = 0; i < cnt; i++)
        {
            float angle = 360f / cnt * i;
            for(int j = 0; j < 8; j += 2)
            {
                float speed = j * 5f + 10f;
                Instantiate(danmaku).AddComponent<ButterflyDanmaku2>().Initialize(position, angle, reverse, speed);
            }
        }
    }

    void ShootArc(GameObject danmaku, Vector3 position, int cnt, bool reverse)
    {
        int total = 28;
        for(int i = 0; i < cnt; i++)
        {
            float angle = 360f / total * i;
            if(reverse)
                angle = 180f - angle;
            for(int j = 0; j < 4; j ++)
            {
                float speed = j * 3.3f + 30f;
                Instantiate(danmaku).AddComponent<ButterflyDanmaku3>().Initialize(position, angle, reverse, speed);
            }
            
        }
    }

}
