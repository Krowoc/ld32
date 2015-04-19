using UnityEngine;
using System.Collections;

public class WordObject : MonoBehaviour {

	public string word;

	public float damage;

	public Vector3 MovementVector;

	public TextMesh textMesh;

	// Use this for initialization
	void Start () 
	{


	}
	
	// Update is called once per frame
	void Update ()
	{
			transform.position += MovementVector * Time.timeScale;
		
	}
	public void SetWord(string word, float damage)
	{
		this.word = word;
		this.damage = damage;

		textMesh = GetComponent<TextMesh>();
		textMesh.text = word;

		MeshRenderer mr = GetComponent<MeshRenderer>();

		BoxCollider2D box = gameObject.AddComponent<BoxCollider2D>();
		float boxBorder = 0.0f;

		box.isTrigger = true;
		box.offset = box.transform.InverseTransformPoint(mr.bounds.center);
		box.size = box.transform.InverseTransformDirection(mr.bounds.size / (transform.lossyScale.magnitude / 1.7f)) + (Vector3.one * boxBorder);
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
