using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteScreenManager : MonoBehaviour
{
    public SpriteRenderer whiteScreen;

    public void Flash(Color color, float outTime, float inTime)
    	=> StartCoroutine(FlashCoroutine(color, outTime, inTime));

    public void FadeOut(Color color, float outTime)
    	=> StartCoroutine(FadeOutCoroutine(color, outTime));

    public void FadeIn(Color color, float inTime)
    	=> StartCoroutine(FadeInCoroutine(color, inTime));

    IEnumerator FlashCoroutine(Color color, float outTime, float inTime)
    {
    	yield return StartCoroutine(FadeOutCoroutine(color, outTime));
    	yield return StartCoroutine(FadeInCoroutine(color, inTime));
    }

    IEnumerator FadeOutCoroutine(Color color, float outTime)
    {
    	for(float t = 0; t < outTime; t += Time.deltaTime)
    	{
    		SetColor(color, t / outTime);
    		yield return 0;
    	}
    	SetColor(color, 1f);
    }

    IEnumerator FadeInCoroutine(Color color, float inTime)
    {
    	for(float t = 0; t < inTime; t += Time.deltaTime)
    	{
    		SetColor(color, 1f - t / inTime);
    		yield return 0;
    	}
    	SetColor(color, 0f);
    }

    void SetColor(Color color, float alpha)
    {
    	color.a = alpha;
    	whiteScreen.color = color;
    }
}
