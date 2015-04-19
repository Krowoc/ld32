using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WordGenerator : MonoBehaviour {

	public TextAsset wordFile;

	public List<Words> wordList;

	public float wordHeightMax = 0.58f;
	public float wordHeightMin = 0.38f;

	public float movementSpeed = 0.2f;

	public float spawnSpeed = 3.0f;

	// Use this for initialization
	void Start () 
	{
		wordList = new List<Words>();

		string[] lineArray = wordFile.text.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);//","[0]);

		for(int i = 1; i < lineArray.Length; i++)
		{
			string[] wordArray = lineArray[i].Split (new string[] { "," }, System.StringSplitOptions.None);

			wordList.Add (new Words(wordArray[0], float.Parse (wordArray[1]), wordArray[2], Int32.Parse (wordArray[3])));
		}


		StartCoroutine ("SpawnWord");

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator SpawnWord() 
	{

		if(Time.timeScale == 0.001f)
			yield return null;
		else

			while(true)
			{
				int rand = Mathf.FloorToInt (UnityEngine.Random.Range(0, wordList.Count));
				
				GameObject wordGO = Instantiate (Resources.Load<GameObject>("Word")) as GameObject;
				
				WordObject word = wordGO.GetComponent<WordObject>();
				
				word.SetMovementVector (new Vector3(-movementSpeed, 0.0f));
				word.SetWord(wordList[rand].word, wordList[rand].damage, wordList[rand].power);
				
				float randHeight = UnityEngine.Random.Range (wordHeightMin, wordHeightMax);
				word.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width, Screen.height * randHeight, -Camera.main.transform.position.z) );
				
				yield return new WaitForSeconds(spawnSpeed);
			}
	}


}
