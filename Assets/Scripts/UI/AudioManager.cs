using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip BGM;
    public AudioClip BGM2;

    public AudioClip StartBGM;
    
	public AudioClip cardSE;

    public AudioClip grazeSE;

    public AudioClip damageSE;

	AudioSource bgm = null;

	public void PlayBGM()
	{
		bgm = gameObject.AddComponent<AudioSource>();
		bgm.clip = BGM;
    	bgm.loop = true;
    	bgm.volume = 1f;
    	bgm.Play();
	}

    public void StopBGM()
    {
        Destroy(bgm);
        bgm = null;
    }

    public void PlayBGM2()
    {
        bgm = gameObject.AddComponent<AudioSource>();
        bgm.clip = BGM2;
        bgm.loop = true;
        bgm.volume = 1f;
        bgm.Play();
    }

    public void PlayStartBGM()
    {
        Debug.Log("play start BGM");
        bgm = gameObject.AddComponent<AudioSource>();
        bgm.clip = StartBGM;
        bgm.loop = true;
        bgm.volume = 1f;
        bgm.Play();
    }

	public float BGMTime()
	{
		if(bgm == null)
			return -1;
		return bgm.time;
	}
    public bool SetBGMTime(float time)
    {
        if (bgm == null)
            return false;
        bgm.time = time;
        return true;
    }
    public void PlayCardSE()
    	=> StartCoroutine(PlayCardSECoroutine());

    IEnumerator PlayCardSECoroutine()
    {
    	AudioSource audioSource = gameObject.AddComponent<AudioSource>();
    	audioSource.clip = cardSE;
    	audioSource.loop = false;
    	audioSource.volume = 0.8f;
    	audioSource.Play();
    	yield return new WaitWhile(() => audioSource.isPlaying);
    	Destroy(audioSource);
    }

    public void PlayGrazeSE(GameObject obj)
        => StartCoroutine(PlayGrazeSECoroutine(obj));
    
    IEnumerator PlayGrazeSECoroutine(GameObject obj)
    {
    	AudioSource audioSource = obj.AddComponent<AudioSource>();
    	audioSource.clip = grazeSE;
    	audioSource.loop = false;
    	audioSource.volume = 1.2f;
        audioSource.spatialBlend = 0.5f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
    	audioSource.Play();
    	yield return new WaitWhile(() => audioSource != null && audioSource.isPlaying);
        if(audioSource != null)
        	Destroy(audioSource);
    }

    public void PlayDamageSE()
    => StartCoroutine(PlayDamageSECoroutine());
    
    IEnumerator PlayDamageSECoroutine()
    {
    	AudioSource audioSource = gameObject.AddComponent<AudioSource>();
    	audioSource.clip = damageSE;
    	audioSource.loop = false;
    	audioSource.volume = 1.0f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
    	audioSource.Play();
    	yield return new WaitWhile(() => audioSource.isPlaying);
    	Destroy(audioSource);
    }
}
