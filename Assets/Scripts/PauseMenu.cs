using UnityEngine;
using System.Collections;
public class PauseMenu : MonoBehaviour
{
	public Sprite pauseMenuSprite;
	public Font pauseMenuFont;
	private bool pauseEnabled = false;	
	private bool showOptionsDropDown = false;

   void Start()
   {
	pauseEnabled = false;
	Time.timeScale = 1;
	AudioListener.volume = 1;
	UnityEngine.Cursor.visible = false;
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
			Time.timeScale = 1;
			AudioListener.volume = 1;
			UnityEngine.Cursor.visible = false;			
		}
		
		//else if game isn't paused, then pause it
		else if(pauseEnabled == false)
		{
			pauseEnabled = true;
			AudioListener.volume = 0;
			Time.timeScale = 0;
			UnityEngine.Cursor.visible = true;
		}
	}
  }


  void OnGUI()
 {
	
 }
}