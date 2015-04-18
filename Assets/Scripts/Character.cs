using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public float mood;

	public float jumpForce = 1.0f;
	public float currentJumpForce;
	public float gravity = -0.3f;
	public bool jumping = false;
	public float groundPosition;

	public Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		groundPosition = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Jump"))
		{
			StartCoroutine ("Jump");
		}

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

	IEnumerator Jump()
	{
		currentJumpForce = jumpForce;

		Vector3 pos = transform.position;
		
		pos += new Vector3(0.0f, currentJumpForce);
		
		transform.position = pos;
		

		while(transform.position.y > groundPosition)
		{
			yield return null;

			currentJumpForce -= gravity;

			Vector3 pos2 = transform.position;

			pos2 += new Vector3(0.0f, currentJumpForce);

			transform.position = pos2;

			if(transform.position.y < groundPosition)
				transform.position = new Vector3(transform.position.x, groundPosition);
		}

	}


}
