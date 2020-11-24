using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card4 : MonoBehaviour
{
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

        yield return new WaitForSeconds(5f);

        yield return new WaitForSeconds(4f);
        GetComponent<Card5>().StartCard();
        Destroy(this);
    }

}
