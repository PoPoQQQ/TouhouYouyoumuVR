using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip BGM;
    public AudioClip BGM2;
    
	public AudioClip cardSE;

    public AudioClip grazeSE;

    public AudioClip damageSE;

    public AudioClip[] danmakuSEs;

    public AudioClip enepSE;

	AudioSource bgm = null;

	public void PlayBGM()
        => StartCoroutine("PlayBGMCoroutine");

    IEnumerator PlayBGMCoroutine()
	{
		bgm = gameObject.AddComponent<AudioSource>();
		bgm.clip = BGM;
    	bgm.loop = true;
    	bgm.volume = 1f;
    	bgm.Play();

        while(true)
        {
            if(bgm.time >= 267f)
                bgm.time = 13.5f;
            yield return 0;
        }
	}

    public void StopBGM()
    {
        StopCoroutine("PlayBGMCoroutine");
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

    public void PauseBGM()
    {
        if(bgm == null)
            return;
        bgm.Pause();
    }

    public void ResumeBGM()
    {
        if(bgm == null)
            return;
        bgm.Play();
    }

    public void FadeOut(float duration)
        => StartCoroutine(FadeOutCoroutine(duration));

    IEnumerator FadeOutCoroutine(float duration)
    {
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            bgm.volume = 1 - t / duration;
            yield return 0;
        }
        bgm.volume = 0;
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

    public void PlayDanmakuSE(int index, float volume = 0.8f)
        => StartCoroutine(PlayDanmakuSECoroutine(index, volume));

    IEnumerator PlayDanmakuSECoroutine(int index, float volume)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = danmakuSEs[index];
        audioSource.loop = false;
        audioSource.volume = volume;
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        Destroy(audioSource);
    }

    public void PlayEnepSE()
    => StartCoroutine(PlayEnepSECoroutine());
    
    IEnumerator PlayEnepSECoroutine()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = enepSE;
        audioSource.loop = false;
        audioSource.volume = 0.8f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        Destroy(audioSource);
    }
}
