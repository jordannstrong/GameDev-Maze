using UnityEngine;
using System.Collections;

public class Chase : MonoBehaviour {
	private GameObject player;
	private float distance;
	//public Transform initialPosition;
	public float moveSpeed;
	public float visionRange;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		//transform.position = initialPosition.position;
	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance (transform.position, player.transform.position);

		if (distance < visionRange) {
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, moveSpeed * Time.deltaTime);
		}
	}
}
