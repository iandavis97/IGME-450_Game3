using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFX : MonoBehaviour {

	public static UISFX instance;//Singleton

	public AudioClip scroll, select;
	public AudioClip bell2, bell3;
	private AudioSource audi;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		audi = GetComponent<AudioSource>();
	}

	public void UISound(bool isSelect) {
		if (isSelect) { // Selection sound
			audi.PlayOneShot(select);
		} else { 
			audi.PlayOneShot(scroll);
		}
	}

	public void Bell(int tolls) {
		if (tolls == 2) { // 2 tolls for round start
			audi.PlayOneShot(bell2);
		} else { // 3 tolls for KO
			audi.PlayOneShot(bell3);
		}
	}
}
