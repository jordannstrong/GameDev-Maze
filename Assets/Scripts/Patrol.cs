﻿using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
	// Initially available on script
	public Transform[] patrolPoints;
	public float moveSpeed;
	private int currentPoint;

	// Used for delay mechanic
	private float timer = 0.0f; 
	public float delay; 

	// Used for rage mechanic
	private GameObject player;
	private float distance;
	private float speedModifier = 2.0f;
	public float visionRange;


	// Use this for initialization
	void Start () {
		transform.position = patrolPoints[0].position;
		currentPoint = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		player = GameObject.FindGameObjectWithTag ("Player");

		distance = Vector3.Distance (transform.position, player.transform.position);

		if (timer > delay) {
			if (transform.position == patrolPoints [currentPoint].position) {
				timer = 0.0f;
				currentPoint++;
			}

			if (currentPoint >= patrolPoints.Length) {
				currentPoint = 0;
			}

			if (distance < visionRange) {
				transform.position = Vector3.MoveTowards (transform.position, patrolPoints [currentPoint].position, moveSpeed * speedModifier * Time.deltaTime);
			} else {
				transform.position = Vector3.MoveTowards (transform.position, patrolPoints [currentPoint].position, moveSpeed * Time.deltaTime);
			}
		}
	}
}
