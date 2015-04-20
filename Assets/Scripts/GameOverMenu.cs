using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenu : MonoBehaviour {

	public Text score;
	// Use this for initialization
	void Start () {
		score = GameObject.Find ("Score").GetComponent<Text>();
		score.text = Globals.playerScore.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickReturnToMainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}
}
