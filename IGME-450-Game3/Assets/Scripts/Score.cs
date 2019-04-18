using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static Score instance = null;
    public static int p1Score;
    public static int p2Score;
    public static int multiplier;

    public Text introMessage; // Message telling the player the objective.
    public Text winMessage; // The message displaying that the player has won.
    public Text scoreMessage;

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
        scoreMessage.enabled = false;
		p1Score = 0;
		p2Score = 0;
        multiplier = 1;
	}

	// Starts the round intro.
	public void ActivateRoundIntro() {
		StartCoroutine(RoundIntro());
	}

    // Update is called once per frame
    void Update ()
    {
		if ((p1Score >= TARG_SCORE || p2Score >= TARG_SCORE) && !isWon) {
			StartCoroutine(Win());
		}
	}
    public static void SetMultiplier(int value)
    {
        multiplier = value;
    }
    //increases score by passed in value, should be dependent on body part hit or other factors
    public static void IncreaseP1Score(int value)
    {
        value *= multiplier;
       p1Score += value;
    }
    //increases score by passed in value, should be dependent on body part hit or other factors
    public static void IncreaseP2Score(int value)
    {
        value *= multiplier;
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
    public void ActivateScoreMessage(GameObject player, int score)
    {
        StartCoroutine(ScoreMessage(player, score));
    }
    private IEnumerator ScoreMessage(GameObject player, int score)//head will reference a player position to put text near
    {
        scoreMessage.enabled = true;
        //making score float
        scoreMessage.transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y+1.0f,
            player.transform.position.z);
        scoreMessage.text = "+" +score;
        yield return new WaitForSeconds(2f);
        scoreMessage.enabled = false;
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
