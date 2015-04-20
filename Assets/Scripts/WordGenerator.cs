using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WordGenerator : MonoBehaviour {

	public int playerScore = 0;

	public TextAsset level0File;
	public TextAsset level1File;
	public TextAsset level2File;
	public TextAsset level3File;

	public List<Words> wordList;
	public List<Level> levelList;
	public Level currentLevel;

	public float wordHeightMax = 0.58f;
	public float wordHeightMin = 0.38f;

	/*public float movementSpeed = 0.2f;

	public float spawnSpeed = 3.0f;

	public float levelLength = 30;*/

	public int level = 0;
	public int maxLevel = 3;
	public float levelLength = 30.0f;

	// Use this for initialization
	void Start () 
	{
		levelList = new List<Level>();

		levelList.Add (new Level(ParseWordFile(level0File), 0.9f, 3.0f, levelLength));
		levelList.Add (new Level(ParseWordFile(level1File), 1.2f, 2.5f, levelLength));
		levelList.Add (new Level(ParseWordFile(level2File), 1.5f, 2.0f, levelLength));
		levelList.Add (new Level(ParseWordFile(level3File), 1.8f, 1.0f, levelLength));

		currentLevel = levelList[0];

		StartCoroutine ("SpawnWord");
		StartCoroutine ("ChangeLevel");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator SpawnWord() 
	{

		while(true)
		{
			int rand = Mathf.FloorToInt (UnityEngine.Random.Range(0, currentLevel.wordList.Count));
			
			GameObject wordGO = Instantiate (Resources.Load<GameObject>("Word")) as GameObject;
			
			WordObject word = wordGO.GetComponent<WordObject>();
			
			word.SetMovementVector (new Vector3(-currentLevel.movementSpeed, 0.0f));
			word.SetWord(currentLevel.wordList[rand].word, currentLevel.wordList[rand].damage, currentLevel.wordList[rand].power);
			
			float randHeight = UnityEngine.Random.Range (wordHeightMin, wordHeightMax);
			word.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width, Screen.height * randHeight, -Camera.main.transform.position.z) );

			float randTime = UnityEngine.Random.Range (-1.0f, 1.0f);

			yield return new WaitForSeconds(currentLevel.spawnSpeed + randTime);
		}
	}

	IEnumerator ChangeLevel()
	{
		for (level = 0; level <= maxLevel; level++)
		{
			Debug.Log ("Level" + level.ToString ());
			currentLevel = levelList[level];
			yield return new WaitForSeconds(currentLevel.levelLength);
		}
	}

	public List<Words> ParseWordFile(TextAsset wFile)
	{
		List<Words> wList = new List<Words>();
		
		string[] lineArray = wFile.text.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);//","[0]);
		
		for(int i = 1; i < lineArray.Length; i++)
		{
			string[] wordArray = lineArray[i].Split (new string[] { "," }, System.StringSplitOptions.None);
			
			wList.Add (new Words(wordArray[0], float.Parse (wordArray[1]), wordArray[2], Int32.Parse (wordArray[3])));
		}

		return wList;
	}
}
