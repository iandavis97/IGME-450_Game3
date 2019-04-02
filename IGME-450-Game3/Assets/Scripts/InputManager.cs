using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For displaying the text string for controls.

public class InputManager : MonoBehaviour {

	// Controls for player 1 and 2.
	public KeyCode p1upArmAdd, p1upArmSub, p1loArmAdd, p1loArmSub, p1Left, p1Right, p2upArmAdd, p2upArmSub, p2loArmAdd, p2loArmSub, p2Left, p2Right;
	public PlayerControl p1, p2; // PlayerControl objects for respective players.

	private Text controls; // Text on the UI that prints out the controls.

	private float torque = 5.0f; // Amount of torque to add to joints.
    private float distance = 0.05f; // Distance to move on walk

    // Grab a reference to the text piece for controls.
    // If Xbox One controller(s) are connected, detect them and override the keyboard controls.
    private void Awake() {
    	controls = Transform.FindObjectOfType<Text>();
    	// #TODO Xbox Controller support
    }

    // Displays controls to the user.
    private void Start() {
		controls.text = "Player 1";
		controls.text += ControlString(p1upArmAdd, p1upArmSub, p1loArmAdd, p1loArmSub, p1Left, p1Right);
		controls.text += "\nPlayer 2";
		controls.text += ControlString(p2upArmAdd, p2upArmSub, p2loArmAdd, p2loArmSub, p2Left, p2Right);
    }

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
        if (Input.GetKey(p1Left)) {
            p1.Walk(-distance);
        } else if (Input.GetKey(p1Right)) {
            p1.Walk(distance);
        }
        if (Input.GetKey(p2Left)) {
            p2.Walk(-distance);
        } else if (Input.GetKey(p2Right)) {
            p2.Walk(distance);
        }
	}

	/** Helper function that builds a string of the keycodes required to control the player.
	 * param[upArmAdd, upArmSub, loArmAdd, loArmSub, left, right] - keycodes for a player's movement.
	 * return - a string containing keycode information for controlling a player.
	 */
	private string ControlString(KeyCode upArmAdd, KeyCode upArmSub, KeyCode loArmAdd, KeyCode loArmSub, KeyCode left, KeyCode right) {
		string result = "";
		result += "\n" + upArmAdd.ToString() + " / " + upArmSub.ToString() + " - UPPER ARM";
		result += "\n" + loArmAdd.ToString() + " / " + loArmSub.ToString() + " - LOWER ARM";
		result += "\n";
		result += "\n" + left.ToString() + " - MOVE LEFT";
		result += "\n" + right.ToString() + " - MOVE RIGHT";
		return result;
	}
}
