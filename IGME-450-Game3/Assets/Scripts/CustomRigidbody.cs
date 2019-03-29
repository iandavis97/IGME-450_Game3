using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * attach this script to any object with a rigidbody that you want a non standard center of mass on
 * currently doesnt work as intended
 */

[RequireComponent(typeof(Rigidbody2D))]
public class CustomRigidbody : MonoBehaviour {

    public Vector3 centerOfMass;
    public float drawSize = 0.01f;
    private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        centerOfMass = myRigidbody.centerOfMass;
	}
	
	// Update is called once per frame
	void Update () {
        myRigidbody.centerOfMass = centerOfMass;
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(centerOfMass, drawSize);
    }
}
