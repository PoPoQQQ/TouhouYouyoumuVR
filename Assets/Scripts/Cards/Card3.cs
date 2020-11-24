using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card3 : MonoBehaviour
{
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

        yield return new WaitForSeconds(5f);

        yield return new WaitForSeconds(4f);
        GetComponent<NonCard4>().StartCard();
        Destroy(this);
    }

}
