using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WordGenerator : MonoBehaviour {

	public TextAsset wordFile;

	public List<Words> wordList;

	//public Dictionary<string,float> wordList;

	public float wordHeightMax = 0.58f;
	public float wordHeightMin = 0.38f;

	public float movementSpeed = 0.2f;

	public float spawnSpeed = 3.0f;

	/*public float niceWordsGain = 1;

	public float positiveWordsGain = 0.5f;

	public float meanWordsDamage = -1f;

	public float negativeWordsDamage   = 0.5f;

	private string[] niceWords  = new string[] {"Beautiful", "Charming", "Invaluable", "Gorgeous",
		                                        "Courageous", "Outspoken", "Driven", "Intelligent",
		                                        "Upbeat", "Grateful"};
	private string[] poitiveWords = new string[] { "Kind", "Caring", "Smart", "Funny", "Important",
		                                           "Determined", "Thoughtful", "Well-rounded", 
		                                           "Pleasant"};
	private string[] meanWords = new string[] {"Uncaring", "Narissitic", "Annoying", "Stupid",
	                                           "Forgetful", "Unsightly", "Unpleasant", 
	                                           "The Worst", "A Mistake", "Un-Grateful",
	                                           "Pitiful", "Horrible", "A disappointment", 
		                                       "Needy"};
	private string[] negativeWords = new string[] {"Mean", "Dumb", "Ugly", "Boring", "Lazy",
		                                         "Smelly"};*/


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


		//wordList = new Dictionary<string, float>();

		/*for (int i = 0; i < niceWords.Length; i++) 
		{
		  wordList.Add (niceWords[i], niceWordsGain);
		}

		for (int i = 0; i < poitiveWords.Length; i++) 
		{
			wordList.Add (poitiveWords[i], positiveWordsGain);
		}

		for (int i = 0; i < meanWords.Length; i++) 
		{
			wordList.Add (meanWords[i], meanWordsDamage);
		}

		for (int i = 0; i < negativeWords.Length; i++) 
		{
			wordList.Add (negativeWords[i], negativeWordsDamage);
		}*/


		StartCoroutine ("SpawnWord");

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator SpawnWord() 
	{
		while(true)
		{
			int rand = Mathf.FloorToInt (UnityEngine.Random.Range(0, wordList.Count));
			//string w = wordList[rand];

			//Pick a random word
			/*List<string> keyList = new List<string>(wordList.Keys);
			int rand = Mathf.FloorToInt (Random.Range(0, keyList.Count));
			string w = keyList[rand];
			*/

			GameObject wordGO = Instantiate (Resources.Load<GameObject>("Word")) as GameObject;

			WordObject word = wordGO.GetComponent<WordObject>();

			word.SetMovementVector (new Vector3(-movementSpeed, 0.0f));
			word.SetWord(wordList[rand].word, wordList[rand].damage);

			float randHeight = UnityEngine.Random.Range (wordHeightMin, wordHeightMax);
			word.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width, Screen.height * randHeight, -Camera.main.transform.position.z) );

			yield return new WaitForSeconds(spawnSpeed);
		}
	}


}
