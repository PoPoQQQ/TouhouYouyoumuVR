using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip BGM;
	public AudioClip cardSE;

	AudioSource bgm = null;

	public void PlayBGM()
	{
		bgm = gameObject.AddComponent<AudioSource>();
		bgm.clip = BGM;
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
}
