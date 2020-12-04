using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card3 : MonoBehaviour
{
    GameObject blueTamaDanmaku;

    void Start()
    {
        blueTamaDanmaku = Resources.Load<GameObject>("Prefabs/Danmaku/BlueTamaDanmaku");
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

        StartCoroutine(BlueTamaCoroutine(blueTamaDanmaku));
        yield return new WaitForSeconds(10f);

        yield return new WaitForSeconds(4f);
        GetComponent<NonCard4>().StartCard();
        Destroy(this);
    }

    IEnumerator BlueTamaCoroutine(GameObject danmaku)
    {
        float interval = 1f;
        while(true)
        {
            yield return new WaitForSeconds(interval);
            interval = Mathf.Max(interval * 0.96f, 0.5f);
            StartCoroutine(ShootRings(danmaku));
        }
    }

    IEnumerator ShootRings(GameObject danmaku)
    {
        yield return 0;
    }

}
