using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For displaying the text string for controls.

public class InputManager : MonoBehaviour {

	// Controls for player 1 and 2.
	public KeyCode p1upArmAdd, p1upArmSub, p1loArmAdd, p1loArmSub, p1Left, p1Right, p2upArmAdd, p2upArmSub, p2loArmAdd, p2loArmSub, p2Left, p2Right;
	[SerializeField]
	public float p1upArmSubf, p1loArmSubf, p1leftRight, p2upArmSubf, p2loArmSubf, p2leftRight; // Use floats to express axis-based input.
	public PlayerControl p1, p2; // PlayerControl objects for respective players.

	private Text controls; // Text on the UI that prints out the controls.

	private float torque = 5.0f; // Amount of torque to add to joints.
    private float distance = 0.05f; // Distance to move on walk
    private int numjoysticks = 0; // The number of connected joysticks.

    // Grab a reference to the text piece for controls.
    // If Xbox One controller(s) are connected, detect them and override the keyboard controls.
    private void Awake() {
    	controls = Transform.FindObjectOfType<Text>();
    	// #TODO Xbox Controller support
    	string[] joysticks = Input.GetJoystickNames();
    	if (joysticks.Length > 0) { // There are joysticks connected, so assign buttons accordingly.
    		numjoysticks = 1;
    		p1upArmAdd = KeyCode.Joystick1Button4;
			p1loArmAdd = KeyCode.Joystick1Button5;
			if (joysticks.Length > 1) { // Connect the second joystick.
				numjoysticks = 2;
				p2upArmAdd = KeyCode.Joystick2Button4;
				p2loArmAdd = KeyCode.Joystick2Button5;
			}
    	}
    }

    // Displays controls to the user.
    private void Start() {
    	if (numjoysticks <= 0) { // No joysticks
			controls.text = "Player 1";
			controls.text += ControlString(p1upArmAdd, p1upArmSub, p1loArmAdd, p1loArmSub, p1Left, p1Right);
			controls.text += "\nPlayer 2";
			controls.text += ControlString(p2upArmAdd, p2upArmSub, p2loArmAdd, p2loArmSub, p2Left, p2Right);
		} else if (numjoysticks == 1) { // One joystick
			controls.text = "Player 1";
			controls.text += ControlStringJS("LB", "LT", "RB", "RT");
			controls.text += "\nPlayer 2";
			controls.text += ControlString(p2upArmAdd, p2upArmSub, p2loArmAdd, p2loArmSub, p2Left, p2Right);
    	} else if (numjoysticks == 2) { // Two joysticks
			controls.text = "Player 1";
			controls.text += ControlStringJS("LB", "LT", "RB", "RT");
			controls.text = "Player 2";
			controls.text += ControlStringJS("LB", "LT", "RB", "RT");
    	}
    }

	private void Update () {
		// For controllers, read the axis-based input.
		p1upArmSubf = Input.GetAxis("P1Fire1");
		p1loArmSubf = Input.GetAxis("P1Fire2");
		p1leftRight = Input.GetAxis("P1X");
		p2upArmSubf = Input.GetAxis("P2Fire1");
		p2loArmSubf = Input.GetAxis("P2Fire2");
		p2leftRight = Input.GetAxis("P2X");
		// Player 1 Controls.
		if (Input.GetKey(p1upArmAdd)) {
			p1.ControlUpperArm(torque);
		} else if (Input.GetKey(p1upArmSub) || p1upArmSubf != 0) {
			p1.ControlUpperArm(-torque);
		}
		if (Input.GetKey(p1loArmAdd)) {
			p1.ControlLowerArm(torque);
		} else if (Input.GetKey(p1loArmSub) || p1loArmSubf != 0) {
			p1.ControlLowerArm(-torque);
		}
        if (Input.GetKey(p1Left) || p1leftRight < 0) {
            p1.Walk(-distance);
		} else if (Input.GetKey(p1Right) || p1leftRight > 0) {
            p1.Walk(distance);
        }
		// Player 2 Controls.
		if (Input.GetKey(p2upArmAdd)) {
			p2.ControlUpperArm(torque);
		} else if (Input.GetKey(p2upArmSub) || p2upArmSubf != 0) {
			p2.ControlUpperArm(-torque);
		}
		if (Input.GetKey(p2loArmAdd)) {
			p2.ControlLowerArm(torque);
		} else if (Input.GetKey(p2loArmSub) || p2loArmSubf != 0) {
			p2.ControlLowerArm(-torque);
		}
        if (Input.GetKey(p2Left) || p2leftRight < 0) {
            p2.Walk(-distance);
		} else if (Input.GetKey(p2Right) || p2leftRight > 0) {
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

	/** Helper function that builds a string of the Xbox buttons required to control the player.
	 * param[upArmAdd, upArmSub, loArmAdd, loArmSub] - strings for a player's movement.
	 * return - a string containing keycode information for controlling a player.
	 */
	private string ControlStringJS(string upArmAdd, string upArmSub, string loArmAdd, string loArmSub) {
		string result = "";
		result += "\n" + upArmAdd + " / " + upArmSub.ToString() + " - UPPER ARM";
		result += "\n" + loArmAdd + " / " + loArmSub.ToString() + " - LOWER ARM";
		result += "\n";
		result += "\n" + "LEFT STICK - MOVE LEFT / RIGHT";
		return result;
	}
}
