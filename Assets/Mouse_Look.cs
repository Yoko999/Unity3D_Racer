using UnityEngine;
using System.Collections;

public class Mouse_Look : MonoBehaviour {

	[SerializeField]
	float Smoothness=3;
	[SerializeField]
	Vector2 Sensitivity =new Vector2(4,4);
	private Vector2 NewCoord;
	public Vector2 CurrentCoord;
	[SerializeField]
	Vector2 Limit = new Vector2(-79,80);
	private Vector2 velocity;
	
	// Update is called once per frame
	void Update () {
		NewCoord.x = Mathf.Clamp (NewCoord.x, Limit.x, Limit.y);
		NewCoord.x -= Input.GetAxis ("Mouse Y") * Sensitivity.x;
		NewCoord.y += Input.GetAxis ("Mouse X") * Sensitivity.y;
		CurrentCoord.x = Mathf.SmoothDamp (CurrentCoord.x, NewCoord.x, ref velocity.x, Smoothness / 100);
		CurrentCoord.y = Mathf.SmoothDamp (CurrentCoord.y, NewCoord.y, ref velocity.y, Smoothness / 100);
		transform.rotation = Quaternion.Euler (CurrentCoord.x, CurrentCoord.y, 0);
	}
}
