using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Some notes on hinge joints
 * The limits on joints are relative to its start position, the green circle displayed in editor does not rotate to match it. Set all limits based on its starting alignment, negative degrees work just fine.
 * Joints dont support adding force directly so I'm working on a way to customize the rigidbodies' center of mass so that we have more control
 * I'm also working on a way to have friction and make the joints return to a starting orientation
 * I'm also working on a way to have the code get all of the refrences to various parts of the player so the editor isnt a mess
 * Left right movement isn't in this build but it should be pretty easy to just move the master player object for that
 */


public class PlayerControl : MonoBehaviour {

    public bool debug = true; //this is just to have an easy way to enable and disable a bunch of logging
    [Header("Refrences To Joint Objects")]
    public HingeJoint2D waistJoint;
    public HingeJoint2D neckJoint;
    public HingeJoint2D shoulderJoint;
    public HingeJoint2D elbowJoint;

    [Header("Refrences To Body Parts")]
    public Rigidbody2D head;
    public Rigidbody2D torso;
    public Rigidbody2D upperArm;
    public Rigidbody2D lowerArm;
    public Rigidbody2D legs;

	// Use this for initialization
	void Start () {
        if (debug)
        {
            Debug.Log("The " + waistJoint.attachedRigidbody.name + " bone is connected to the " + waistJoint.connectedBody.name + " bone");
            Debug.Log("The " + neckJoint.attachedRigidbody.name + " bone is connected to the " + neckJoint.connectedBody.name + " bone");
            Debug.Log("The " + shoulderJoint.attachedRigidbody.name + " bone is connected to the " + shoulderJoint.connectedBody.name + " bone");
            Debug.Log("The " + elbowJoint.attachedRigidbody.name + " bone is connected to the " + elbowJoint.connectedBody.name + " bone");
        }
	}
	
	// Update is called once per frame
	void Update () {
        //temporary input code until we have something
        if (Input.GetKey(KeyCode.LeftArrow)) { upperArm.AddTorque(-5.0f); }
        if (Input.GetKey(KeyCode.RightArrow)) { upperArm.AddTorque(5.0f); }
        if (Input.GetKey(KeyCode.UpArrow)) { lowerArm.AddTorque(-5.0f); }
        if (Input.GetKey(KeyCode.DownArrow)) { lowerArm.AddTorque(5.0f); }
	}
}
