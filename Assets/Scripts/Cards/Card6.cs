using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card6 : MonoBehaviour
{
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

        yield return new WaitForSeconds(100000f);

        yield return new WaitForSeconds(4f);
        GetComponent<Card6>().StartCard();
        Destroy(this);
    }

}
