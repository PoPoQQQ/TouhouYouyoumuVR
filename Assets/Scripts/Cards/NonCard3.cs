using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCard3 : MonoBehaviour
{
    public void StartCard()
    	=> StartCoroutine(CardCoroutine());

    IEnumerator CardCoroutine()
    {
        GetComponent<WhiteScreenManager>().Flash(Color.white, 0.5f, 0.5f);
        GetComponent<SaigyoujiYuyuko>().MoveTo(new Vector3(0f, 10f, 100f));
        yield return new WaitForSeconds(0.5f);
        GetComponent<SaigyoujiYuyuko>().Ring(false);
        GetComponent<BackgroundManager>().SetBackground(3);
        yield return new WaitForSeconds(3f);

        yield return new WaitForSeconds(5f);

        yield return new WaitForSeconds(4f);
        GetComponent<Card3>().StartCard();
        Destroy(this);
    }
}
