using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenu : MonoBehaviour {

	public Text scoreText;
	public Text hiScoreText;
	float score;
	float hiScore;

	// Use this for initialization
	void Start () {

		//Get Score and hi score
		score = Globals.playerScore * 10;
		hiScore = PlayerPrefs.GetFloat ("HiScore", score);

		//Update hi score if needed
		if(score > hiScore)
		{
			hiScore = score;
			PlayerPrefs.SetFloat ("HiScore", hiScore);
		}

		//Display scores
		scoreText = GameObject.Find ("Score").GetComponent<Text>();
		scoreText.text = score.ToString ();

		hiScoreText = GameObject.Find ("HiScore").GetComponent<Text>();
		hiScoreText.text = hiScore.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		//This is for testing the hi score
		if(Input.GetKeyDown (KeyCode.R))
		{
			PlayerPrefs.DeleteKey ("HiScore");
			Debug.Log ("Hi Score Reset");
		}
	}

	public void OnClickReturnToMainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}
}
