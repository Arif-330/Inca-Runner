using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
	public AudioClip[] audioClips;
	public AudioSource efxSource;
	public AudioSource musicSource;

	public static AudioManager instance = null;
	
	public float lowPitchRange = .95f;
	public float highPitchRange = 1.05f;
	
	
	
	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		}else if(instance != this){
			Destroy(instance);
		}
		DontDestroyOnLoad (gameObject);
	}

	
	public void PlaySound(AudioClip clip){
		efxSource.clip = clip;
		efxSource.Play ();
	}
	
	public void RandomizeSfx(params AudioClip [] clips){
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);
		
		efxSource.pitch = randomPitch;
		efxSource.clip = clips [randomIndex];
		efxSource.Play ();
	}
}
