using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	public int rank=0;
	public GUIText RankGUI;

	void OnTriggerEnter(Collider col){
		if (col.tag != "Player")
			++rank;
		else
			RankGUI.text = "Поздравляем!\nВы заняли "+rank+" место!";
	}
}
