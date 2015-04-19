using UnityEngine;
using System.Collections;

public class Words {

	public string word;
	public float damage;
	public string power;
	public int level;

	public Words(string w, float d, string p, int l)
	{
		word = w;
		damage = d;
		power = p;
		level = l;
	}
}
