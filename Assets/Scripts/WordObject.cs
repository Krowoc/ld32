using UnityEngine;
using System.Collections;

public class WordObject : MonoBehaviour {

	public string word;

	public float damage;

	public string power;

	public Vector3 MovementVector;

	public TextMesh textMesh;

	BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () 
	{


	}
	
	// Update is called once per frame
	void Update ()
	{
			transform.position += MovementVector * Time.timeScale;
		
	}
	public void SetWord(string word, float damage, string power)
	{
		this.word = word;
		this.damage = damage;
		this.power = power;

		textMesh = GetComponent<TextMesh>();
		textMesh.text = word;

		MeshRenderer mr = GetComponent<MeshRenderer>();

		boxCollider = gameObject.AddComponent<BoxCollider2D>();
		float boxBorder = 0.0f;

		boxCollider.isTrigger = true;
		boxCollider.offset = boxCollider.transform.InverseTransformPoint(mr.bounds.center);
		boxCollider.size = boxCollider.transform.InverseTransformDirection(mr.bounds.size / (transform.lossyScale.magnitude / 1.7f)) + (Vector3.one * boxBorder);
	}

	public void SetMovementVector(Vector3 v3)
	{
			MovementVector = v3;
	}

	void OnBecameInvisible() 
	{
		GameObject.Destroy (gameObject);
	}

	public void Remove()
	{
		boxCollider.enabled = false;

		StartCoroutine ("FadeOut");
	}

	IEnumerator FadeOut()
	{
		float fadeSpeed = 0.08f;
		MeshRenderer mr = GetComponent<MeshRenderer>();
		Color c = mr.material.color;

		while (c.a > 0)
		{
			c = new Color(c.r, c.g, c.b, c.a -= fadeSpeed);
			mr.material.color = c;

			yield return null;
		}
		GameObject.Destroy (gameObject);
	}
}
