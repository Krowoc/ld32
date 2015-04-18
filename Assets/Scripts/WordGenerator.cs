using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordGenerator : MonoBehaviour {

	public Dictionary<string,float> wordList;

	public float groundHeight = 0.5f;

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
			List<string> keyList = new List<string>(wordList.Keys);

			//Random rand = new Random();
			int rand = Mathf.FloorToInt (Random.Range(0, keyList.Count));
			string w = keyList[rand];
			//float d = wordList[w];
			//Debug.Log (rand);
			//KeyValuePair<string, float> w = wordList.ElementAt(rand);
			//string w = wordList[rand];

			GameObject wordGO = Instantiate (Resources.Load<GameObject>("Word")) as GameObject;

			WordObject word = wordGO.GetComponent<WordObject>();

			word.SetMovementVector (new Vector3(-movementSpeed, 0.0f));
			word.SetWord(w, wordList[w]);
			word.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width, Screen.height * groundHeight, -Camera.main.transform.position.z) );

			yield return new WaitForSeconds(spawnSpeed);
		}
	}


}
