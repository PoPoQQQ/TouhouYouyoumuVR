using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEffectManager2 : MonoBehaviour
{
	public GameObject yuyukoTachie;
	public Text[] textMeshes;
	public GameObject cardName;
	public Image cardNameFrame;
    public GameObject words;

    public void StartCard(string cardName, bool last = false)
    {
    	float duration = 2.7f;
        GameObject.Find("Player").GetComponentInChildren<AudioManager>().PlayCardSE();
        if(!last)
        {
            StartCoroutine(YuyukoTachieMove(duration));
            StartCoroutine(YuyukoTachieFade(duration));
        }
        else
            words.SetActive(false);
    	foreach(Text textMesh in textMeshes)
    		textMesh.text = cardName;
    	StartCoroutine(CardNameAnimation());
    }

     public void EndCard()
        => SetCardNameAlpha(0f);

    IEnumerator YuyukoTachieMove(float duration)
    {
    	float amplitude = 200;
    	for(float t = 0; t < duration; t += Time.deltaTime)
    	{
    		SetTachieY(amplitude * (1 - 2 * t / duration));
    		yield return 0;
    	}
    }

    IEnumerator YuyukoTachieFade(float duration)
    {
    	float amplitude = 0.8f;
    	for(float t = 0; t < duration; t += Time.deltaTime)
    	{
    		float x = t / duration;
    		SetTachieAlpha(-4f * (x * x - x) * amplitude);
    		yield return 0;
    	}
        SetTachieAlpha(0f);
    }

    IEnumerator CardNameAnimation()
    {
    	float duration1 = 0.5f;
    	SetCardNameY(-294f);
    	for(float t = 0; t < duration1; t += Time.deltaTime)
    	{
    		SetCardNameScale(1.2f + 2f * (1 - t / duration1));
    		SetCardNameAlpha(t / duration1);
    		yield return 0;
    	}
    	SetCardNameScale(1.2f);
    	SetCardNameAlpha(1f);

    	yield return new WaitForSeconds(1.7f);

    	float duration2 = 0.5f;
    	for(float t = 0; t < duration2; t += Time.deltaTime)
    	{
    		SetCardNameY(-294f * (1 - t / duration2) + 467f * (t / duration2));
    		yield return 0;
    	}
    	SetCardNameY(467f);
    }

    void SetTachieY(float y)
    {
    	Vector3 position = yuyukoTachie.transform.localPosition;
    	position.y = y;
    	yuyukoTachie.transform.localPosition = position;
    }

    void SetTachieAlpha(float alpha)
    {
    	Color color = yuyukoTachie.GetComponent<Image>().color;
    	color.a = alpha;
    	yuyukoTachie.GetComponent<Image>().color = color;
    }

    void SetCardNameScale(float scale)
    {
        Vector3 localScale = cardName.transform.localScale;
        localScale.x = localScale.y = scale;
        cardName.transform.localScale = localScale;
    }

    void SetCardNameY(float y)
    {
    	Vector3 position = cardName.transform.localPosition;
    	position.y = y;
    	cardName.transform.localPosition = position;
    }

    void SetCardNameAlpha(float alpha)
    {
    	foreach(Text text in textMeshes)
    	{
    		Color color = text.color;
	    	color.a = alpha;
	    	text.color = color;
    	}
    	{
    		Color color = cardNameFrame.color;
	    	color.a = alpha;
	    	cardNameFrame.color = color;
    	}
    }

    public void WordsAppear()
        => StartCoroutine(WordsAppearCoroutine());

    IEnumerator WordsAppearCoroutine()
    {
        float duration = 2f;
        float initialX = 10f;

        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            Vector3 position = words.transform.position;
            position.x = initialX * (1 - t / duration);
            words.transform.position = position;
            Color color = words.GetComponent<SpriteRenderer>().color;
            color.a = t / duration;
            words.GetComponent<SpriteRenderer>().color = color;
            yield return 0;
        }
        {
            Vector3 position = words.transform.position;
            position.x = 0;
            words.transform.position = position;
            Color color = words.GetComponent<SpriteRenderer>().color;
            color.a = 1;
            words.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
