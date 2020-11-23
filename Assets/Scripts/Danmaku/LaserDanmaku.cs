using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDanmaku : MonoBehaviour
{
	Transform sprite;
	float loadingTime;
	float width;
	const float initialWidth = 1.5f;

	public void Initialize(float loadingTime, float width)
	{
		this.loadingTime = loadingTime;
		this.width = width;
		sprite = GetComponentInChildren<SpriteRenderer>().transform;
		StartCoroutine(Widen());
	}

	public void Destruct()
	{
		StartCoroutine(DestructCoroutine());
	}

	IEnumerator Widen()
	{
		Vector3 scale = sprite.localScale;
		scale.y = initialWidth;
		sprite.localScale = scale;

		yield return new WaitForSeconds(loadingTime);

		float duration = 0.3f;
		for(float t = 0; t < duration; t += Time.deltaTime)
		{
			scale.y = initialWidth + (t / duration) * (width - initialWidth);
			sprite.localScale = scale;
			yield return 0;
		}

		scale.y = width;
		sprite.localScale = scale;
	}

	IEnumerator DestructCoroutine()
	{
		Vector3 scale = sprite.localScale;

		float duration = 0.3f;
		for(float t = 0; t < duration; t += Time.deltaTime)
		{
			scale.y = (1 - t / duration) * width;
			sprite.localScale = scale;
			yield return 0;
		}
		Destroy(gameObject);
	}
}
