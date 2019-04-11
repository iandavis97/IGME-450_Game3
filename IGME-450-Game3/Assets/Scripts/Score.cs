using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static Score instance = null;
    public static float p1Score;
    public static float p2Score;

    public Text introMessage; // Message telling the player the objective.
    public Text winMessage; // The message displaying that the player has won.

    private bool isWon = false; // Whether or not the round has been won.
    private const int TARG_SCORE = 100; // Number of points to win the game.

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
	}

	void Start() {
		isWon = false;
		winMessage.enabled = false;
		p1Score = 0;
		p2Score = 0;
		StartCoroutine(RoundIntro());
	}

    // Update is called once per frame
    void Update ()
    {
		if ((p1Score >= TARG_SCORE || p2Score >= TARG_SCORE) && !isWon) {
			StartCoroutine(Win());
		}
	}
    //increases score by passed in value, should be dependent on body part hit or other factors
    public static void IncreaseP1Score(int value)
    {
       p1Score += value;
    }
    //increases score by passed in value, should be dependent on body part hit or other factors
    public static void IncreaseP2Score(int value)
    {
        p2Score += value;
    }

    private IEnumerator RoundIntro() {
    	introMessage.enabled = true;
		introMessage.text = "First to " + TARG_SCORE + " points wins...";
		yield return new WaitForSeconds(3f);
		introMessage.text = "FIGHT!";
		yield return new WaitForSeconds(1f);
		introMessage.enabled = false;
    }

    private IEnumerator Win() {
    	isWon = true;
    	winMessage.enabled = true;
    	if (p1Score > p2Score) { // P1 has won
    	 	winMessage.text = "PLAYER 1 WINS!";
    	} else { // P2 has won
			winMessage.text = "PLAYER 2 WINS!";
    	}
    	yield return new WaitForSeconds(3f);
    	SceneManager.LoadScene("2Players");
    }
}
