using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody))]
public class Enemy : MonoBehaviour {

	public float speed;
	public Vector3 movement;
	private Boundary bound;	
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		bound = new Boundary ();
		speed = Random.Range(5.0f, bound.vMax);
		GameObject en=GameObject.Find("Enemies");
		rb = GetComponent<Rigidbody>();
		rb.detectCollisions = true;
		rb.SetDensity (5);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.rotation = Quaternion.Euler(0, 0, 0);
		movement = this.transform.position;
	
		if (movement.z <= bound.roadMaxLen) {
			float newZ = movement.z + speed * Time.deltaTime;		
			this.transform.position = new Vector3 (movement.x, movement.y, newZ);
		}
	}

	void FixedUpdate() {
		rb.AddRelativeForce (0, 0, 5);
	}

}
