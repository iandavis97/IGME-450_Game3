using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour {

	public AudioClip charSelect;
	public AudioClip[] music; 
	private AudioSource audi;

	public static Jukebox instance;//Singleton

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		audi = GetComponent<AudioSource>();
		audi.clip = charSelect;
		audi.Play();
	}

	public void Play() {
		audi.Stop();
		audi.clip = (music[Random.Range(0, music.Length)]);
		audi.loop = true;
		audi.Play();
	}

	public void CallFade() {
		StartCoroutine("Fade");
	}

	private IEnumerator Fade() {
		while (audi.volume > 0) {
			audi.volume -= 0.05f;
			yield return new WaitForEndOfFrame();
		}
	}
}
