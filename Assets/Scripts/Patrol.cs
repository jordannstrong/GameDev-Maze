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
	public float visionRange;
	public float turnSpeed;
	private float speedModifier = 2.0f;



	public void InitializeInfo () {
		player = GameObject.FindGameObjectWithTag ("Player");
		transform.position = patrolPoints[0].position;
		currentPoint = 0;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

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
				var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
				transform.position = Vector3.MoveTowards(transform.position, patrolPoints [currentPoint].position, moveSpeed * speedModifier * Time.deltaTime);
			} else {
				var targetRotation = Quaternion.LookRotation(patrolPoints[currentPoint].position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
				transform.position = Vector3.MoveTowards(transform.position, patrolPoints [currentPoint].position, moveSpeed * Time.deltaTime);
			}
		}
	}	
}
