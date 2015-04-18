using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public float mood;

	public Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(Input.GetKey (KeyCode.T))
		{
			Debug.Log ("Test");
			mood = -1.0f;
		}
		//else
		//	mood = 0.0f;


		animator.SetFloat ("Mood", mood);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Hit!");

		WordObject wo = other.GetComponent<WordObject>();
		if(wo != null)
		{
			mood += wo.damage;
		}
	}
}
