using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card5 : MonoBehaviour
{
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

        yield return new WaitForSeconds(5f);

        GetComponent<Card6>().StartCard();
        Destroy(this);
    }

}
