using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level {

	public List<Words> wordList;
	public float movementSpeed;
	public float spawnSpeed;
	public float levelLength;

	public Level(List<Words> wordList, float movementSpeed, float spawnSpeed, float levelLength)
	{
		this.wordList = wordList;
		this.movementSpeed = movementSpeed;
		this.spawnSpeed = spawnSpeed;
		this.levelLength = levelLength;
	}
}
