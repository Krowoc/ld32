using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public float mood;
	public float maxMood;
	public float minMood;
	public bool failed = false;

	public float jumpForce = 1.0f;
	public float currentJumpForce;
	public float gravity = -0.3f;
	public float groundPosition;
	public bool rolling = false;
	public bool jumping = false;

	public Animator animator;

	public AudioSource music;

	public float musicNormal = 1.0f;
	public float musicSlow = 0.75f;
	public float musicTransitionSpeed = 2.0f;

	public float score;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		groundPosition = transform.position.y;

		music = GameObject.Find ("AudioSource").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.timeScale == Globals.PauseSpeed)
			return;

		if(failed)
			return;

		//Jumping
		if(Input.GetButtonDown("Jump") || Input.GetAxis ("Vertical") > 0)
		{
			if(jumping == false && rolling == false)
			{
				StartCoroutine ("Jump");
			}
		}

		//Rolling
		if(Input.GetAxis ("Vertical") < 0)
		{
			if(jumping == false)
			{
				rolling = true;
				animator.SetBool ("Rolling", true);
			}
		}
		else 
			animator.SetBool("Rolling", false);

		animator.SetFloat ("Mood", mood);
		animator.SetBool ("Jumping", jumping);

		if(mood < -2.0f)
		{
			music.pitch = Mathf.Lerp(music.pitch, musicSlow, musicTransitionSpeed * Time.deltaTime);
		}
		else
		{
			music.pitch = Mathf.Lerp(music.pitch, musicNormal, musicTransitionSpeed * Time.deltaTime);
		}

		score = Globals.playerScore;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		WordObject wo = other.GetComponent<WordObject>();
		if(wo != null)
		{
			if(wo.damage > 0.0f)
				Globals.playerScore += wo.damage;

			mood += wo.damage;
			mood = Mathf.Clamp (mood, minMood, maxMood);
		}

		wo.Remove ();

		if(mood < -3.0f)
		{
			StartCoroutine ("Fail");
		}
	}

	//Called from animation when it ends
	public void FinishRolling()
	{
		rolling = false;
	}

	IEnumerator Jump()
	{
		//If already jumping, break
		//if (transform.position.y > groundPosition)
		//	yield break;

		jumping = true;
		currentJumpForce = jumpForce;

		Vector3 pos = transform.position;
		
		pos += new Vector3(0.0f, currentJumpForce);
		
		transform.position = pos;
		

		while(transform.position.y > groundPosition)
		{
			yield return null;

			currentJumpForce -= gravity * Time.timeScale;

			Vector3 pos2 = transform.position;

			pos2 += new Vector3(0.0f, currentJumpForce) * Time.timeScale;

			transform.position = pos2;

			if(transform.position.y < groundPosition)
				transform.position = new Vector3(transform.position.x, groundPosition);
		}

		jumping = false;
	}


	IEnumerator Fail()
	{
		failed = true;
		animator.SetBool ("Failed", failed);

		yield return new WaitForSeconds(3.0f);

		Application.LoadLevel("GameOver");
	}
}
