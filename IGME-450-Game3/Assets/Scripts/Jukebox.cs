using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour {

	public AudioClip[] music; 
	private AudioSource audi;

	public static Jukebox instance;//Singleton

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		audi = GetComponent<AudioSource>();
	}

	public void Play() {
		audi.PlayOneShot(music[Random.Range(0, music.Length)]);
	}
}
