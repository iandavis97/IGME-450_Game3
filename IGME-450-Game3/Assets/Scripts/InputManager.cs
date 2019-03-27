using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	// Controls for player 1 and 2.
	public KeyCode p1upArmAdd, p1upArmSub, p1loArmAdd, p1loArmSub, p2upArmAdd, p2upArmSub, p2loArmAdd, p2loArmSub;
	public PlayerControl p1, p2; // PlayerControl objects for respective players.

	private float torque = 5.0f; // Amount of torque to add to joints.

	private void Update () {
		// Player 1 Controls.
		if (Input.GetKey(p1upArmAdd)) {
			p1.ControlUpperArm(torque);
		} else if (Input.GetKey(p1upArmSub)) {
			p1.ControlUpperArm(-torque);
		}
		if (Input.GetKey(p1loArmAdd)) {
			p1.ControlLowerArm(torque);
		} else if (Input.GetKey(p1loArmSub)) {
			p1.ControlLowerArm(-torque);
		}
	}
}
