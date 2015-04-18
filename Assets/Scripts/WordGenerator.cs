using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordGenerator : MonoBehaviour {

	public Dictionary<string,float> wordList;

	public float wordHeightMax = 0.58f;
	public float wordHeightMin = 0.38f;

	public float movementSpeed = 0.2f;

	public float spawnSpeed = 3.0f;

	// Use this for initialization
	void Start () 
	{
		wordList = new Dictionary<string, float>();
		wordList.Add ("Good", 1.0f);
		wordList.Add ("Bad", -1.0f);


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
			//Pick a random word
			List<string> keyList = new List<string>(wordList.Keys);
			int rand = Mathf.FloorToInt (Random.Range(0, keyList.Count));
			string w = keyList[rand];

			GameObject wordGO = Instantiate (Resources.Load<GameObject>("Word")) as GameObject;

			WordObject word = wordGO.GetComponent<WordObject>();

			word.SetMovementVector (new Vector3(-movementSpeed, 0.0f));
			word.SetWord(w, wordList[w]);

			float randHeight = Random.Range (wordHeightMin, wordHeightMax);
			word.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width, Screen.height * randHeight, -Camera.main.transform.position.z) );

			yield return new WaitForSeconds(spawnSpeed);
		}
	}


}
