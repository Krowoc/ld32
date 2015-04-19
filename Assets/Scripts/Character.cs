using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public float mood;
	public float maxMood;
	public float minMood;

	public float jumpForce = 1.0f;
	public float currentJumpForce;
	public float gravity = -0.3f;
	public float groundPosition;
	public bool jumping = false;

	public Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		groundPosition = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.timeScale == 0.001f)
			return;
		else
			if(Input.GetButtonDown("Jump"))
			{
				StartCoroutine ("Jump");
			}

		animator.SetFloat ("Mood", mood);
		animator.SetBool ("Jumping", jumping);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		WordObject wo = other.GetComponent<WordObject>();
		if(wo != null)
		{
			mood += wo.damage;
			mood = Mathf.Clamp (mood, minMood, maxMood);
		}

		wo.Remove ();
	}

	IEnumerator Jump()
	{
		//If already jumping, break
		if (transform.position.y > groundPosition)
			yield break;

		jumping = true;
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

		jumping = false;
	}


}
