using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
	SpriteRenderer spriteRenderer;

    public float rotateSpeed;

    public float alphaBase;
    public float alphaAmplitude;
    public float alphaFrequency;

    public float scaleBase;
    public float scaleAmplitude;
    public float scaleFrequency;

    void Start()
    {
    	spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);

        Color color = spriteRenderer.color;
        color.a = alphaBase + Mathf.Sin(alphaFrequency * Time.time) * alphaAmplitude;
        spriteRenderer.color = color;

        float scale = scaleBase + Mathf.Sin(scaleFrequency * Time.time) * scaleAmplitude;
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
