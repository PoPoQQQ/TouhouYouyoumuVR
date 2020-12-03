using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanmakuSelfdestruct : MonoBehaviour
{
	void Start()
	{
		DanmakuManager.Register(gameObject);
	}

    void Update()
    {
        if(transform.position.z > 150f || transform.position.z < -20f)
        	StartCoroutine(FadeOut(0.2f));
        if(((Vector2)transform.position).magnitude > 95f)
        	StartCoroutine(FadeOut(0.2f));
    }

    bool fadeout = false;
    IEnumerator FadeOut(float duration)
    {
        if(fadeout)
            yield break;
        fadeout = true;
        DanmakuManager.Unregister(gameObject);
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            Color color = spriteRenderer.color;
            color.a = 1 - t / duration;
            spriteRenderer.color = color;
            yield return 0;
        }
        Destroy(gameObject);
    }
}
