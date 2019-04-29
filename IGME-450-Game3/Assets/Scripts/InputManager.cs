using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For displaying the text string for controls.
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

	// Controls for player 1 and 2.
	public KeyCode p1upArmAdd, p1upArmSub, p1loArmAdd, p1loArmSub, p1Left, p1Right, p2upArmAdd, p2upArmSub, p2loArmAdd, p2loArmSub, p2Left, p2Right; // Note: p1upArmAdd and p1loArmAdd (and p2 variants) are buttons to leave character select.
	[SerializeField]
	public float p1upArmSubf, p1loArmSubf, p1leftRight, p2upArmSubf, p2loArmSubf, p2leftRight; // Use floats to express axis-based input.
	public KeyCode start, quit, start2, quit2; // Start2 and Quit2 are for the second player controller.
	public PlayerControl p1, p2; // PlayerControl objects for respective players.

	public Text controls; // Text on the UI that prints out the controls.
    public Text p1ScoreText;//Text on UI that prints player 1's score
    public Text p2ScoreText;//Text on UI that prints player 2's score

    public float p1Score;
    public float p2Score;

    private float torque = 5.0f; // Amount of torque to add to joints.
    private float distance = 1.5f; // Distance to move on walk
    private int numjoysticks = 0; // The number of connected joysticks.

    private bool inGame; // Allows use of InputManager outside of fight

    private string selectString = "";

    // Grab a reference to the text piece for controls.
    // If Xbox One controller(s) are connected, detect them and override the keyboard controls.
    private void Awake() {
    	// #TODO Xbox Controller support
    	string[] joysticks = Input.GetJoystickNames();
    	if (joysticks.Length > 0) { // There are joysticks connected, so assign buttons accordingly.
    		numjoysticks = 1;
    		p1upArmAdd = KeyCode.Joystick1Button4;
			p1loArmAdd = KeyCode.Joystick1Button5;
			quit = KeyCode.Joystick1Button1;
			start = KeyCode.Joystick1Button0;
			if (joysticks.Length > 1) { // Connect the second joystick.
				numjoysticks = 2;
				p2upArmAdd = KeyCode.Joystick2Button4;
				p2loArmAdd = KeyCode.Joystick2Button5;
				quit2 = KeyCode.Joystick2Button1;
				start2 = KeyCode.Joystick2Button0;
			}
    	}
    }

    // Displays controls to the user.
    private void Start() {
    	selectString += "PRESS ";
    	if (numjoysticks <= 0) { // No joysticks
			controls.text = "Player 1";
			controls.text += ControlString(p1upArmAdd, p1upArmSub, p1loArmAdd, p1loArmSub, p1Left, p1Right);
			controls.text += "\n\nPlayer 2";
			controls.text += ControlString(p2upArmAdd, p2upArmSub, p2loArmAdd, p2loArmSub, p2Left, p2Right);
			selectString += "\nP1: " + p1Left.ToString() + " - " + p1Right.ToString() + " - SCROLL\n";
			selectString += p1upArmSub.ToString() + " - " + p1loArmSub.ToString() + " - BACK\n";
			selectString += p1upArmAdd.ToString() + " - " + p1loArmAdd.ToString() + " - CONFIRM\n";
			selectString += "\nP2: " + p2Left.ToString() + " - " + p2Right.ToString() + " - SCROLL\n";
			selectString += p2upArmSub.ToString() + " - " + p2loArmSub.ToString() + " - BACK\n";
			selectString += p2upArmAdd.ToString() + " - " + p2loArmAdd.ToString() + " - CONFIRM\n";
			selectString += "ESC TO QUIT";
		} else if (numjoysticks == 1) { // One joystick
			controls.text = "Player 1";
			controls.text += ControlStringJS("LB", "LT", "RB", "RT");
			controls.text += "\n\nPlayer 2";
			controls.text += ControlString(p2upArmAdd, p2upArmSub, p2loArmAdd, p2loArmSub, p2Left, p2Right);
			selectString += "\nP1: " + "LB - RB - CONFIRM\n LT - RT - BACK\n";
			selectString += "\nP2: " + p2Left.ToString() + " - " + p2Right.ToString() + " - SCROLL\n";
			selectString += p2upArmSub.ToString() + " - " + p2loArmSub.ToString() + " - BACK\n";
			selectString += p2upArmAdd.ToString() + " - " + p2loArmAdd.ToString() + " - CONFIRM\n";
			selectString += "ESC OR B BUTTON TO QUIT";
    	} else if (numjoysticks == 2) { // Two joysticks
			controls.text = "Controls";
			controls.text += ControlStringJS("LB", "LT", "RB", "RT");
			selectString += "\nLB - RB - CONFIRM\n LT - RT - BACK\n";
			selectString += "B BUTTON TO QUIT";
    	}
        inGame = false;
    }


	public string GetSelectString() {
		return selectString;
	}

	private void Update () {
		// We need to detect joystick inputs before the game starts.
		p1leftRight = Input.GetAxis("P1X");
		p2leftRight = -Input.GetAxis("P2X"); // Inverted due to -x scale
		// For controllers, read the axis-based input.
            p1upArmSubf = Input.GetAxis("P1Fire1");
            p1loArmSubf = Input.GetAxis("P1Fire2");
            p2upArmSubf = Input.GetAxis("P2Fire1");
            p2loArmSubf = Input.GetAxis("P2Fire2");
        // Detect input for quitting and starting the game on the title screen.
		if ((Input.GetKeyDown(quit) || Input.GetKeyDown(quit2)) && SceneManager.GetActiveScene().name.Equals("MainMenu")) {
			Application.Quit();
		} else if ((Input.GetKeyDown(start) || Input.GetKeyDown(start2)) && SceneManager.GetActiveScene().name.Equals("MainMenu")) {
			SceneManager.LoadScene("2Players");
		}
        // only happen if in-game
        if (inGame)
        {

                                                 // Player 1 Controls.
            if (Input.GetKey(p1upArmAdd))
            {
                p1.ControlUpperArm(torque);
            }
            else if (Input.GetKey(p1upArmSub) || p1upArmSubf != 0)
            {
                p1.ControlUpperArm(-torque);
            }
            if (Input.GetKey(p1loArmAdd))
            {
                p1.ControlLowerArm(torque);
            }
            else if (Input.GetKey(p1loArmSub) || p1loArmSubf != 0)
            {
                p1.ControlLowerArm(-torque);
            }
            if (Input.GetKey(p1Left) || p1leftRight < 0)
            {
                p1.Walk(-distance);
            }
            else if (Input.GetKey(p1Right) || p1leftRight > 0)
            {
                p1.Walk(distance);
            }
            // Player 2 Controls.
            if (Input.GetKey(p2upArmAdd))
            {
                p2.ControlUpperArm(torque);
            }
            else if (Input.GetKey(p2upArmSub) || p2upArmSubf != 0)
            {
                p2.ControlUpperArm(-torque);
            }
            if (Input.GetKey(p2loArmAdd))
            {
                p2.ControlLowerArm(torque);
            }
            else if (Input.GetKey(p2loArmSub) || p2loArmSubf != 0)
            {
                p2.ControlLowerArm(-torque);
            }
            if (Input.GetKey(p2Left) || p2leftRight < 0)
            {
                p2.Walk(-distance);
            }
            else if (Input.GetKey(p2Right) || p2leftRight > 0)
            {
                p2.Walk(distance);
            }

            //updating score
            p1Score = Score.p1Score;
            p2Score = Score.p2Score;

            //display score to user
            p1ScoreText.text = p1Score.ToString();
            p2ScoreText.text = p2Score.ToString();
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

    // allows outside activation of game state
    public void GameStart()
    {
		Score.instance.ActivateRoundIntro();
        inGame = true;
    }
}
