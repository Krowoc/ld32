using UnityEngine;
using System.Collections;
public class PauseMenu : MonoBehaviour
{

	public Canvas pauseMenu;
	private bool pauseEnabled = false;	

   void Start()
   {
	pauseEnabled = false;
	Time.timeScale = 1;
	AudioListener.volume = 1;
	UnityEngine.Cursor.visible = false;
	pauseMenu = pauseMenu.GetComponent<Canvas> ();
	pauseMenu.enabled = false;
   }

   void Update()
  {
	
	//check if pause button (escape key) is pressed
	if(Input.GetKeyDown("escape"))
	{
		
		//check if game is already paused		
		if(pauseEnabled == true)
		{
			//unpause the game
			pauseEnabled = false;
			pauseMenu.enabled = false;
			Time.timeScale = 1;
			AudioListener.volume = 1;
			UnityEngine.Cursor.visible = false;			
		}
		
		//else if game isn't paused, then pause it
		else if(pauseEnabled == false)
		{
			pauseEnabled = true;
			pauseMenu.enabled = true;
			AudioListener.volume = 0;
			Time.timeScale = 0.0000001f;
			UnityEngine.Cursor.visible = true;
		}
	}
  }


	public void MainMenu()
	{
		Time.timeScale = 1;
		Application.LoadLevel(0);
	}
}