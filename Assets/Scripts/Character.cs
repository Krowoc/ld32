using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public float mood = 0.7f;
	public float maxMood = 1.0f;
	public float minMood = 0.0f;
	public float damageModifier = 6.0f;
	public float tempMood = 0.7f;
	public float tempMoodAdjust = 0.2f;
	public float tempMoodAlign = 0.004f;
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

	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		groundPosition = transform.position.y;

		music = GameObject.Find ("Music").GetComponent<AudioSource>();

		audioSource = GetComponent<AudioSource>();
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

		if(Input.GetKeyDown (KeyCode.Period))
		{
			Time.timeScale = 4.0f;
		}

		if(Input.GetKeyUp (KeyCode.Period))
		{
			Time.timeScale = 1.0f;
		}

		if(tempMood < mood)
			tempMood += tempMoodAlign;
		if(tempMood > mood)
			tempMood -= tempMoodAlign;
		animator.SetFloat ("Mood", tempMood);
		animator.SetBool ("Jumping", jumping);

		if(mood < 0.2f)
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
		audioSource.Play ();

		WordObject wo = other.GetComponent<WordObject>();
		if(wo != null)
		{
			if(wo.damage > 0.0f)
				Globals.playerScore += wo.damage;

			mood += wo.damage / damageModifier;
			mood = Mathf.Clamp (mood, minMood, maxMood);

			if(wo.damage > 0)
				tempMood = mood + tempMoodAdjust;
			else if(wo.damage < 0)
				tempMood = mood - tempMoodAdjust;
			tempMood = Mathf.Clamp (tempMood, minMood, maxMood);

		}

		wo.Remove ();

		if(mood <= 0.0f)
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
