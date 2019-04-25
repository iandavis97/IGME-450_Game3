using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour {

	private Camera self;
	private const float MAX_SIZE = 5f;
	private const float MIN_SIZE = 3f;
	private float stepSize = 0.001f;
	private bool zoomingIn = true;


	private void Awake() {
		self = GetComponent<Camera>();
	}

	private void Update () {
		if (self.orthographicSize > MIN_SIZE && zoomingIn) {
			transform.Translate(Vector2.right * -stepSize);
			self.orthographicSize -= stepSize;
		} else if (self.orthographicSize > MAX_SIZE && !zoomingIn) {
			zoomingIn = true;
		} else {
			zoomingIn = false;
			transform.Translate(Vector2.right * stepSize);
			self.orthographicSize += stepSize;
		}
	}
}
