using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]

public class NewBehaviourScript : MonoBehaviour {

	private Mouse_Look cam;
	public Vector3 speed;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Mouse_Look>();
	}
	
	// Update is called once per frame
	void LateUpdate (){
		transform.Translate(Vector3.forward*Input.GetAxis("Vertical")*speed.x*Time.deltaTime);
		transform.Translate(Vector3.right*Input.GetAxis("Horizontal")*speed.y*Time.deltaTime);
		if(Input.GetButton("Jump")) GetComponent<Rigidbody>().AddRelativeForce(0, speed.z*100, 0);
	}
}
