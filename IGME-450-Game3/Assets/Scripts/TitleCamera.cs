using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour {

	private Camera self;
	private const float MIN_SIZE = 3f;
	private float stepSize = 0.001f;

	private void Awake() {
		self = GetComponent<Camera>();
	}

	private void Update () {
		if (self.orthographicSize > MIN_SIZE) {
			transform.Translate(Vector2.right * -stepSize);
			self.orthographicSize -= stepSize;
		}
	}
}
